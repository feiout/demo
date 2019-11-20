using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace New.Common
{
    public class DateTimeConverter : DateTimeConverterBase
    {
        private DateTimeStyles _dateTimeStyles = DateTimeStyles.RoundtripKind;
        private const string DefaultDateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.000+08:00";
        private string _dateTimeFormat;
        private CultureInfo _culture;

        public DateTimeStyles DateTimeStyles
        {
            get
            {
                return _dateTimeStyles;
            }
            set
            {
                _dateTimeStyles = value;
            }
        }

        public string DateTimeFormat
        {
            get
            {
                return _dateTimeFormat ?? string.Empty;
            }
            set
            {
                _dateTimeFormat = StringUtils.NullEmptyString(value);
            }
        }

        public CultureInfo Culture
        {
            get
            {
                return _culture ?? CultureInfo.CurrentCulture;
            }
            set
            {
                _culture = value;
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            string str;
            if (value is DateTime)
            {
                var dateTime = (DateTime)value;
                if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal ||
                    (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
                {
                    dateTime = dateTime.ToUniversalTime();
                }
                str = dateTime.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);
            }
            else
            {
                if (!(value is DateTimeOffset))
                {
                    throw new JsonSerializationException(
                        StringUtils.FormatWith(
                            "Unexpected value when converting date. Expected DateTime or DateTimeOffset, got {0}.",
                            CultureInfo.InvariantCulture,
                            ReflectionUtils.GetObjectType(value)));
                }
                var dateTimeOffset = (DateTimeOffset)value;
                if ((_dateTimeStyles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.AdjustToUniversal ||
                    (_dateTimeStyles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.AssumeUniversal)
                {
                    dateTimeOffset = dateTimeOffset.ToUniversalTime();
                }
                str = dateTimeOffset.ToString(_dateTimeFormat ?? DefaultDateTimeFormat, Culture);
            }
            writer.WriteValue(str);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var flag = ReflectionUtils.IsNullableType(objectType);
            var type = flag ? Nullable.GetUnderlyingType(objectType) : objectType;
            if (reader.TokenType == JsonToken.Null)
            {
                if (!ReflectionUtils.IsNullableType(objectType))
                {
                    throw new Exception(StringUtils.FormatWith("Cannot convert null value to {0}.",
                                                               CultureInfo.InvariantCulture));
                    //                    throw JsonSerializationException.Create(reader, StringUtils.FormatWith("Cannot convert null value to {0}.", (IFormatProvider)CultureInfo.InvariantCulture, (object)objectType));
                }
                return null;
            }
            if (reader.TokenType == JsonToken.Date)
            {
                return type == typeof(DateTimeOffset) ? new DateTimeOffset((DateTime)reader.Value) : reader.Value;
            }
            if (reader.TokenType != JsonToken.String)
                throw new Exception(StringUtils.FormatWith("Unexpected token parsing date. Expected String, got {0}.", CultureInfo.InvariantCulture));
            //                    throw JsonSerializationException.Create(reader, StringUtils.FormatWith("Unexpected token parsing date. Expected String, got {0}.", (IFormatProvider)CultureInfo.InvariantCulture, (object)reader.TokenType));
            var str = reader.Value.ToString();
            if (string.IsNullOrEmpty(str) && flag)
            {
                return null;
            }
            if (type == typeof(DateTimeOffset))
            {
                return !string.IsNullOrEmpty(_dateTimeFormat) ?
                    DateTimeOffset.ParseExact(str, _dateTimeFormat, Culture, _dateTimeStyles) :
                    DateTimeOffset.Parse(str, Culture, _dateTimeStyles);
            }
            return !string.IsNullOrEmpty(_dateTimeFormat) ?
                DateTime.ParseExact(str, _dateTimeFormat, Culture, _dateTimeStyles) :
                DateTime.Parse(str, Culture, _dateTimeStyles);
        }

    }
}
