using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using New.Common;
using Newtonsoft.Json;

namespace New.RestUtility
{
    public static class RestHelper
    {
        #region Const Properties

        private static readonly UTF8Encoding Encoder = new UTF8Encoding();

        private const string RequestContentType = "application/json;charset=UTF-8";
        private const string CookieKey = "Set-Cookie";
        private const int UnauthorizedCode = 10002;

        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore,
            DefaultValueHandling = DefaultValueHandling.Ignore,
            DateFormatHandling = DateFormatHandling.IsoDateFormat,
            DateParseHandling = DateParseHandling.DateTime,
            DateTimeZoneHandling = DateTimeZoneHandling.Local
        };

        #endregion

        #region Public Properties

        public static string RestCookie { get; set; }

        #endregion

        #region Static Constructor

        static RestHelper()
        {
            ServicePointManager.DefaultConnectionLimit = 100;
            JsonSerializerSettings.Converters.Add(new DateTimeConverter());
        }

        #endregion

        #region Private Methods For REST

        private static HttpWebRequest InitRequest(string url, string method)
        {
            // 垃圾回收，清理无效链接
            GC.Collect();
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = RequestContentType;
            request.Timeout = RestConfig.ConnectionTimeout;
            request.KeepAlive = false;
            request.Proxy = null;
            request.Headers.Add("Accept-Encoding", "gzip");
            if (request.CookieContainer == null)
            {
                request.CookieContainer = new CookieContainer();
            }
            if (!string.IsNullOrEmpty(RestCookie))
            {
                request.CookieContainer.SetCookies(request.RequestUri, RestCookie);
            }
            return request;
        }

        /// <summary>
        /// 将josn反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns>T</returns>
        private static T DeserializeObject<T>(string json)
        {
            var result = JsonConvert.DeserializeObject<Result<object>>(json);
            //return result == null ? default(T) : result.Data; 
            if (result == null)
            {
                return default(T);
            }
            if (result.Code > 0)
            {
                throw new Exception(result.Message);
            }
            if (result.Data == null)
            {
                return default(T);
            }
            var data = result.IsZipData ? RestConfig.UnGzip(result.Data.ToString()) : result.Data.ToString();
            try
            {
                return JsonConvert.DeserializeObject<T>(data);
            }
            catch (Exception)
            {
                //TODO
                return JsonConvert.DeserializeObject<Result<T>>(json).Data;
            }
        }
        static readonly object synchLock = new object();
        /// <summary>
        /// 请求服务端方法
        /// </summary>
        /// <param name="method"></param>
        /// <param name="url"></param>
        /// <param name="jsonBody"></param>
        /// <returns></returns>
        private static string GetResponseString(string method, string url, string jsonBody = null)
        {
            lock (synchLock)
            {
                var request = InitRequest(url, method);
                if (!string.IsNullOrEmpty(jsonBody))
                {
                    var stream = request.GetRequestStream();
                    using (var writer = new StreamWriter(stream))
                    {
                        writer.Write(jsonBody);
                        writer.Flush();
                        //writer.Close();
                    }
                }
                try
                {
                    var webResponse = request.GetResponse();
                    var response = (HttpWebResponse)webResponse;
                    var statusCode = (int)response.StatusCode;
                    if (statusCode >= 200 && statusCode < 300)
                    {
                        if (string.IsNullOrEmpty(RestCookie))
                        {
                            RestCookie = response.Headers[CookieKey];
                        }
                        return GetResponseString(response);
                    }
                    if (RestConfig.IsUseInEquipment)
                    {
                        RestCookie = string.Empty;
                        return string.Empty;
                    }
                    throw new Exception(response.StatusDescription);
                }
                catch (WebException e)
                {
                    if (RestConfig.IsUseInEquipment || e.Status == WebExceptionStatus.ConnectFailure)
                    {
                        RestCookie = string.Empty;
                        return string.Empty;
                    }
                    throw GenerateException(e);
                }
            }
        }

        /// <summary>
        /// 将 HttpWebResponse 返回结果转换成 string
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        private static string GetResponseString(HttpWebResponse response)
        {
            var json = string.Empty;
            if (response == null)
            {
                return json;
            }
            if (string.IsNullOrEmpty(RestCookie))
            {
                RestCookie = response.Headers[CookieKey];
            }
            var responseStream = response.GetResponseStream();
            if (responseStream == null) return json;
            if (response.ContentEncoding.ToLower().Contains("gzip"))
            {
                using (GZipStream stream = new GZipStream(responseStream, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                    {
                        //json = reader.ReadToEnd();
                        json = GetStringFormStream(reader);
                    }
                }
            }
            else if (response.ContentEncoding.ToLower().Contains("deflate"))
            {
                using (DeflateStream stream = new DeflateStream(responseStream, CompressionMode.Decompress))
                {
                    using (var reader = new StreamReader(stream, Encoding.GetEncoding("UTF-8")))
                    {
                        //json = reader.ReadToEnd(); 
                        json = GetStringFormStream(reader);
                    }
                }
            }
            else
            {
                using (var reader = new StreamReader(responseStream, Encoding.GetEncoding("UTF-8")))
                {
                    json = GetStringFormStream(reader);
                }
            }
            responseStream.Close();
            return json;
        }

        /// <summary>
        /// 从文件流的第index个位置开始读，到count个字符，把它们存到buffer中，然后返回一个正数，内部指针后移一位，保证下次从新的位置开始读。
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        private static string GetStringFormStream(StreamReader reader)
        {
            var json = string.Empty;
            var buffer = new char[2048];
            int index = reader.Read(buffer, 0, 2048);
            while (index > 0)
            {
                json += new String(buffer, 0, index);
                index = reader.Read(buffer, 0, 2048);
            }
            reader.Close();
            return json;
        }

        /// <summary>
        /// 异常处理
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        private static Exception GenerateException(WebException exception)
        {
            var response = GetResponseString((HttpWebResponse)exception.Response);
            try
            {
                var failResult = JsonConvert.DeserializeObject<Result<object>>(response);
                if (failResult != null && failResult.Code == UnauthorizedCode)
                {
                    return new Exception(failResult.Message);
                }
                return new Exception(exception.Message + " - " + response);
            }
            catch (Exception e)
            {
                return new Exception(e.Message + " - " + response);
            }
        }

        #endregion

        #region REST For Object Methods

        #region Get Method

        /// <summary>
        /// REST GET Object
        /// </summary>
        /// <typeparam name="T">要返回的对象</typeparam>
        /// <param name="methodUrl">服务端地址</param>
        /// <returns>返回一个对象</returns>
        public static T Get<T>(string methodUrl)
        {
            return Get<T>(methodUrl, null);
            //var result = JsonConvert.DeserializeObject<Result<T>>(json);
            //return result == null ? default(T) : result.Data; 
        }

        /// <summary>
        /// REST GET Object With Parameters
        /// </summary>
        /// <typeparam name="T">要返回的对象</typeparam>
        /// <param name="methodUrl">服务端地址</param>
        /// <param name="parameters">条件</param>
        /// <returns>返回一个对象</returns>
        public static T Get<T>(string methodUrl, List<KeyValuePair<string, string>> parameters)
        {
            const string method = "GET";
            var url = RestConfig.ServiceRoot + methodUrl;
            if (parameters != null)
            {
                var queryStringBuilder = new StringBuilder();
                var i = 0;
                foreach (var parameter in parameters)
                {
                    queryStringBuilder.AppendFormat(i > 0 ? "&{0}={1}" : "?{0}={1}", parameter.Key,
                                                    Uri.EscapeDataString(parameter.Value));
                    i++;
                }
                url = Encoder.GetString(Encoder.GetBytes(url + queryStringBuilder));
            }
            var json = GetResponseString(method, url);
            return DeserializeObject<T>(json);
        }
        ///// <summary>
        ///// REST GET Object With Object
        ///// </summary>
        ///// <typeparam name="T">传到服务端的对象</typeparam>
        ///// <typeparam name="TR">服务端返回的对象</typeparam>
        ///// <param name="methodUrl">服务端地址</param>
        ///// <param name="postObj">传到服务端的对象</param>
        ///// <returns>返回一个对象</returns>
        //public static TR GetList<TR>(string methodUrl, T postObj)
        //{
        //    const string method = "GET";
        //    var url = RestConfig.ServiceRoot + methodUrl;
        //    var postJson = JsonConvert.SerializeObject(postObj, JsonSerializerSettings);
        //    var json = GetResponseString(method, url, postJson);
        //    return DeserializeObject<TR>(json);
        //}


        /// <summary>
        /// 作废方法，使用 Get T 方法代替
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="methodUrl"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [Obsolete]
        public static T GetOne<T>(string methodUrl, string parameter)
        {
            const string method = "GET";
            var url = RestConfig.ServiceRoot + methodUrl + parameter;
            var json = GetResponseString(method, url);
            return DeserializeObject<T>(json);
        }

        #endregion

        #region Post Method

        /// <summary>
        /// REST POST Object
        /// </summary>
        /// <typeparam name="T">传到服务端的对象</typeparam>
        /// <param name="methodUrl">服务端地址</param>
        /// <param name="postObj">传到服务端的对象</param>
        /// <returns>返回 T 和 传到服务端的是同一对象</returns>
        public static T Post<T>(string methodUrl, T postObj)
        {
            const string method = "POST";
            var url = RestConfig.ServiceRoot + methodUrl;
            var postJson = JsonConvert.SerializeObject(postObj, JsonSerializerSettings);
            var resultJson = GetResponseString(method, url, postJson);
            return DeserializeObject<T>(resultJson);
        }

        /// <summary>
        /// REST POST Object
        /// </summary>
        /// <typeparam name="T">传到服务端的对象</typeparam>
        /// <typeparam name="TR">服务端返回的对象</typeparam>
        /// <param name="methodUrl">服务端地址</param>
        /// <param name="postObj">传到服务端的对象</param>
        /// <returns>返回 TR 对象</returns>
        public static TR Post<T, TR>(string methodUrl, T postObj)
        {
            const string method = "POST";
            var url = RestConfig.ServiceRoot + methodUrl;
            var postJson = JsonConvert.SerializeObject(postObj, JsonSerializerSettings);
            var resultJson = GetResponseString(method, url, postJson);
            return DeserializeObject<TR>(resultJson);
        }

        #endregion

        #region Put Method

        /// <summary>
        /// REST PUT Object
        /// </summary>
        /// <typeparam name="T">传到服务端的对象</typeparam>
        /// <param name="methodUrl">服务端地址</param>
        /// <param name="putObj">传到服务端的对象</param>
        /// <returns>服务端返回 0 ? false : true;</returns>
        public static bool Put<T>(string methodUrl, T putObj)
        {
            const string method = "PUT";
            var url = RestConfig.ServiceRoot + methodUrl;
            var putJson = JsonConvert.SerializeObject(putObj, JsonSerializerSettings);
            return GetResponseString(method, url, putJson) != "0";
        }

        /// <summary>
        /// REST PUT Object
        /// </summary>
        /// <typeparam name="T">传到服务端的对象</typeparam>
        /// <typeparam name="TR">服务端返回的对象</typeparam>
        /// <param name="methodUrl">服务端地址</param>
        /// <param name="postObj">传到服务端的对象</param>
        /// <returns>返回 TR 对象</returns>
        public static TR Put<T, TR>(string methodUrl, T postObj)
        {
            const string method = "PUT";
            var url = RestConfig.ServiceRoot + methodUrl;
            var putJson = JsonConvert.SerializeObject(postObj, JsonSerializerSettings);
            var resultJson = GetResponseString(method, url, putJson);
            return DeserializeObject<TR>(resultJson);
        }

        /// <summary>
        /// REST PUT
        /// </summary>
        /// <param name="metodUrl">服务端地址</param>
        /// <returns>服务端返回 0 ? false : true;</returns>
        public static bool Put(string metodUrl)
        {
            const string method = "PUT";
            var url = RestConfig.ServiceRoot + metodUrl;
            return GetResponseString(method, url) != "0";
        }


        #endregion

        #region Delete Method

        /// <summary>
        /// REST DELETE
        /// </summary>
        /// <param name="metodUrl">服务端地址</param>
        /// <returns>服务端返回 0 ? false : true;</returns>
        public static bool Delete(string metodUrl)
        {
            const string method = "DELETE";
            var url = RestConfig.ServiceRoot + metodUrl;
            return GetResponseString(method, url) != "0";
        }

        /// <summary>
        /// REST DELETE
        /// </summary>
        /// <param name="metodUrl">服务端地址</param>
        /// <param name="parameters">条件参数</param>
        /// <returns>服务端返回 0 ? false : true;</returns>
        public static bool Delete(string metodUrl, List<KeyValuePair<string, string>> parameters)
        {
            const string method = "DELETE";
            var url = RestConfig.ServiceRoot + metodUrl;
            if (parameters != null)
            {
                var queryStringBuilder = new StringBuilder();
                var i = 0;
                foreach (var parameter in parameters)
                {
                    queryStringBuilder.AppendFormat(i > 0 ? "&{0}={1}" : "?{0}={1}", parameter.Key,
                                                    Uri.EscapeDataString(parameter.Value));
                    i++;
                }
                url = Encoder.GetString(Encoder.GetBytes(url + queryStringBuilder));
            }
            return GetResponseString(method, url) != "0";
        }

        /// <summary>
        /// 带返回值的删除
        /// </summary>
        /// <typeparam name="T">返回的对象</typeparam>
        /// <param name="metodUrl">Resource Url</param>
        /// <param name="parameters">条件参数</param>
        /// <returns>返回 T 对象</returns>
        public static T Delete<T>(string metodUrl, List<KeyValuePair<string, string>> parameters = null)
        {
            const string method = "DELETE";
            var url = RestConfig.ServiceRoot + metodUrl;
            if (parameters != null)
            {
                var queryStringBuilder = new StringBuilder();
                var i = 0;
                foreach (var parameter in parameters)
                {
                    queryStringBuilder.AppendFormat(i > 0 ? "&{0}={1}" : "?{0}={1}", parameter.Key,
                                                    Uri.EscapeDataString(parameter.Value));
                    i++;
                }
                url = Encoder.GetString(Encoder.GetBytes(url + queryStringBuilder));
            }
            var resultJson = GetResponseString(method, url);
            return DeserializeObject<T>(resultJson);
        }

        #endregion

        #endregion
    }
}
