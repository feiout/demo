using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;


namespace New.RestUtility
{
    public static class RestConfig
    {
        public static string GetAppConfig(string key)
        {
            var configValue = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrEmpty(configValue))
            {
                configValue = string.Empty;
            }
            return configValue;
        }


        private static string _serviceRoot = string.Empty;
        public static string ServiceRoot
        {
            get
            {
                if (string.IsNullOrEmpty(_serviceRoot))
                {
                    _serviceRoot = GetLocalSetting("ServiceRoot");
                    if (string.IsNullOrEmpty(_serviceRoot))
                    {
                        _serviceRoot = GetAppConfig("ServiceRoot");
                    }
                    return _serviceRoot;
                }
                return _serviceRoot;
            }
        }

        private static bool? _isUseInEquipment;
        public static bool IsUseInEquipment
        {
            get
            {
                if (_isUseInEquipment == null)
                {
                    var isUsing = GetAppConfig("EquipmentNo");
                    _isUseInEquipment = !string.IsNullOrEmpty(isUsing);
                }
                return _isUseInEquipment != null && (bool)_isUseInEquipment;
            }
        }

        private static int _connectionTimeout = 0;
        /// <summary>
        /// 连接超时时间（单位：秒）
        /// </summary>
        public static int ConnectionTimeout
        {
            get
            {
                if (_connectionTimeout == 0)
                {
                    int.TryParse(GetAppConfig("ConnectionTimeout"), out _connectionTimeout);
                }
                return (_connectionTimeout == 0 ? 30 : _connectionTimeout) * 1000;
            }
        }

        public static string UnGzip(string zippedString)
        {
            if (string.IsNullOrEmpty(zippedString) || zippedString.Length == 0)
            {
                return string.Empty;
            }
            var zippedData = Convert.FromBase64String(zippedString);
            return System.Text.Encoding.UTF8.GetString(UnGzip(zippedData));
        }

        private static byte[] UnGzip(byte[] zippedData)
        {
            var ms = new MemoryStream(zippedData);
            var compressedzipStream = new GZipStream(ms, CompressionMode.Decompress);
            var outBuffer = new MemoryStream();
            var block = new byte[1024];
            while (true)
            {
                var bytesRead = compressedzipStream.Read(block, 0, block.Length);
                if (bytesRead <= 0)
                {
                    break;
                }
                outBuffer.Write(block, 0, bytesRead);
            }
            compressedzipStream.Close();
            return outBuffer.ToArray();
        }

        /// <summary>
        /// 本地配置
        /// </summary>
        private static Dictionary<string, string> _localConfigsMap = new Dictionary<string, string>();

        /// <summary>
        /// 加载本地配置
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetLocalSetting(string key)
        {
            if (_localConfigsMap == null || _localConfigsMap.Count == 0)
            {
                LoadLocalConfig();
            }
            if (_localConfigsMap == null || _localConfigsMap.Count == 0)
            {
                return String.Empty;
            }
            if (!_localConfigsMap.ContainsKey(key))
            {
                return String.Empty;
            }
            return _localConfigsMap[key];
        }

        /// <summary>
        /// 本地配置保存路径
        /// </summary>
        private const string LocalConfigurationPath = "setting.db";

        /// <summary>
        /// 加载本地配置记录
        /// </summary>
        private static void LoadLocalConfig()
        {
            if (!File.Exists(LocalConfigurationPath))
            {
                return;
            }
            var utf8Encoding = new UTF8Encoding(false);
            using (var sr = new StreamReader(LocalConfigurationPath, utf8Encoding))
            {
                var localConfigJson = sr.ReadToEnd();
                if (!string.IsNullOrEmpty(localConfigJson))
                {
                    try
                    {
                        //                        var decodeConfigJson = SecurityHelper.Des3Decrypt(localConfigJson);
                        _localConfigsMap = JsonConvert.DeserializeObject<Dictionary<string, string>>(localConfigJson);
                    }
                    catch (Exception)
                    {
                        _localConfigsMap = new Dictionary<string, string>();
                    }
                    //StaticCacheHelper<Dictionary<string, string>>.Set(LocalConfigCacheGroup, _localConfigsMap);
                }
            }
        }

    }
}
