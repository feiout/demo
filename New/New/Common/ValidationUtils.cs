using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New.Common
{
    public static class ValidationUtils
    {
        public static void ArgumentNotNullOrEmpty(string value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
            if (value.Length == 0)
                throw new ArgumentException(StringUtils.FormatWith("'{0}' cannot be empty.", CultureInfo.InvariantCulture, parameterName), parameterName);
        }

        public static void ArgumentTypeIsEnum(Type enumType, string parameterName)
        {
            ArgumentNotNull(enumType, "enumType");
            if (!TypeExtensions.IsEnum(enumType))
                throw new ArgumentException(StringUtils.FormatWith("Type {0} is not an Enum.", CultureInfo.InvariantCulture, enumType), parameterName);
        }

        public static void ArgumentNotNullOrEmpty<T>(ICollection<T> collection, string parameterName)
        {
            ArgumentNotNullOrEmpty(collection, parameterName, StringUtils.FormatWith("Collection '{0}' cannot be empty.", CultureInfo.InvariantCulture, parameterName));
        }

        public static void ArgumentNotNullOrEmpty<T>(ICollection<T> collection, string parameterName, string message)
        {
            if (collection == null)
                throw new ArgumentNullException(parameterName);
            if (collection.Count == 0)
                throw new ArgumentException(message, parameterName);
        }

        public static void ArgumentNotNull(object value, string parameterName)
        {
            if (value == null)
                throw new ArgumentNullException(parameterName);
        }

        public static void ArgumentConditionTrue(bool condition, string parameterName, string message)
        {
            if (!condition)
                throw new ArgumentException(message, parameterName);
        }
    }
}
