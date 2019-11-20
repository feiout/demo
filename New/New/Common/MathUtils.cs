using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New.Common
{
    public class MathUtils
    {
        public static int IntLength(int i)
        {
            if (i < 0)
                throw new ArgumentOutOfRangeException();
            return i == 0 ? 1 : (int)Math.Floor(Math.Log10(i)) + 1;
        }

        public static char IntToHex(int n)
        {
            return n <= 9 ? (char)(n + 48) : (char)(n - 10 + 97);
        }

        public static int? Min(int? val1, int? val2)
        {
            if (!val1.HasValue)
                return val2;
            return !val2.HasValue ? val1 : Math.Min(val1.Value, val2.Value);
        }

        public static int? Max(int? val1, int? val2)
        {
            if (!val1.HasValue)
                return val2;
            return !val2.HasValue ? val1 : Math.Max(val1.Value, val2.Value);
        }

        public static double? Max(double? val1, double? val2)
        {
            if (!val1.HasValue)
                return val2;
            return !val2.HasValue ? val1 : Math.Max(val1.Value, val2.Value);
        }

        public static bool ApproxEquals(double d1, double d2)
        {
            if (d1 == d2)
                return true;
            double num1 = (Math.Abs(d1) + Math.Abs(d2) + 10.0) * 2.22044604925031E-16;
            double num2 = d1 - d2;
            return -num1 < num2 && num1 > num2;
        }
    }
}
