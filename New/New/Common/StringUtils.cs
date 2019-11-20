using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace New.Common
{
    public static class StringUtils
    {
        public const string CarriageReturnLineFeed = "\r\n";
        public const string Empty = "";
        public const char CarriageReturn = '\r';
        public const char LineFeed = '\n';
        public const char Tab = '\t';

        public static string FormatWith(this string format, IFormatProvider provider, object arg0)
        {
            return FormatWith(format, provider, new []{arg0});
        }

        public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1)
        {
            return FormatWith(format, provider, new []{arg0,arg1});
        }

        public static string FormatWith(this string format, IFormatProvider provider, object arg0, object arg1, object arg2)
        {
            return FormatWith(format, provider, new []{arg0,arg1,arg2});
        }

        public static string FormatWith(this string format, IFormatProvider provider, params object[] args)
        {
            ValidationUtils.ArgumentNotNull(format, "format");
            return string.Format(provider, format, args);
        }

        public static bool IsWhiteSpace(string s)
        {
            if (s == null)
                throw new ArgumentNullException("s");
            if (s.Length == 0)
                return false;
            for (int index = 0; index < s.Length; ++index)
            {
                if (!char.IsWhiteSpace(s[index]))
                    return false;
            }
            return true;
        }

        public static string NullEmptyString(string s)
        {
            return !string.IsNullOrEmpty(s) ? s : null;
        }

        public static StringWriter CreateStringWriter(int capacity)
        {
            return new StringWriter(new StringBuilder(capacity), CultureInfo.InvariantCulture);
        }

        public static int? GetLength(string value)
        {
            return value == null ? new int?() : value.Length;
        }

        public static string ToCharAsUnicode(char c)
        {
            return new string(new[]
                {
                    '\\',
                    'u',
                    MathUtils.IntToHex(c >> 12 & 15),
                    MathUtils.IntToHex(c >> 8 & 15),
                    MathUtils.IntToHex(c >> 4 & 15),
                    MathUtils.IntToHex(c & 15)
                });
        }

        public static TSource ForgivingCaseSensitiveFind<TSource>(this IEnumerable<TSource> source, Func<TSource, string> valueSelector, string testValue)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (valueSelector == null)
                throw new ArgumentNullException("valueSelector");
            IEnumerable<TSource> source1 = Enumerable.Where(source, (s => string.Equals(valueSelector(s), testValue, StringComparison.OrdinalIgnoreCase)));
            if (Enumerable.Count(source1) <= 1)
                return Enumerable.SingleOrDefault(source1);
            return Enumerable.SingleOrDefault(Enumerable.Where(source, s => string.Equals(valueSelector(s), testValue, StringComparison.Ordinal)));
        }

        public static string ToCamelCase(string s)
        {
            if (string.IsNullOrEmpty(s) || !char.IsUpper(s[0]))
                return s;
            string str = char.ToLower(s[0], CultureInfo.InvariantCulture).ToString(CultureInfo.InvariantCulture);
            if (s.Length > 1)
                str = str + s.Substring(1);
            return str;
        }

        public static bool IsHighSurrogate(char c)
        {
            return char.IsHighSurrogate(c);
        }

        public static bool IsLowSurrogate(char c)
        {
            return char.IsLowSurrogate(c);
        }
    }
}
