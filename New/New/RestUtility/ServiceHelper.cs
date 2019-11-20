using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace New.RestUtility
{
    public sealed class ServiceHelper<T>
    {
        public static T CreateInterface()
        {
            var tName = typeof(T).Name;
            return GetActionInterface(tName);
        }

        private static readonly Dictionary<string, T> MCache = new Dictionary<string, T>();
        private static List<Type> _serviceTypes = new List<Type>();
        private static T GetActionInterface(string key)
        {
            if (!MCache.ContainsKey(key))
            {
                MCache.Add(key, (T)Activator.CreateInstance(typeof(T), new object[] { }));
            }
            return MCache[key];
        }

    }
}
