using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace New.Common
{
    public static class CollectionUtils
    {
        public static IEnumerable<T> CastValid<T>(this IEnumerable enumerable)
        {
            ValidationUtils.ArgumentNotNull(enumerable, "enumerable");
            return Enumerable.Cast<T>(Enumerable.Where(Enumerable.Cast<object>(enumerable), o => o is T));
        }

        public static bool IsNullOrEmpty<T>(ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        public static void AddRange<T>(this IList<T> initial, IEnumerable<T> collection)
        {
            if (initial == null)
            {
                throw new ArgumentNullException("initial");
            }
            if (collection == null)
            {
                return;
            }
            foreach (T obj in collection)
            {
                initial.Add(obj);
            }
        }

        public static void AddRange(this IList initial, IEnumerable collection)
        {
            ValidationUtils.ArgumentNotNull(initial, "initial");
            AddRange(new ListWrapper<object>(initial), Enumerable.Cast<object>(collection));
        }

        public static IList CreateGenericList(Type listType)
        {
            ValidationUtils.ArgumentNotNull(listType, "listType");
            return (IList)ReflectionUtils.CreateGeneric(typeof(List<>), listType, new object[0]);
        }

        public static bool IsDictionaryType(Type type)
        {
            ValidationUtils.ArgumentNotNull(type, "type");
            return typeof(IDictionary).IsAssignableFrom(type) || ReflectionUtils.ImplementsGenericDefinition(type, typeof(IDictionary<,>));
        }

        public static IWrappedCollection CreateCollectionWrapper(object list)
        {
            ValidationUtils.ArgumentNotNull(list, "list");
            Type collectionDefinition;
            if (ReflectionUtils.ImplementsGenericDefinition(list.GetType(), typeof(ICollection<>),
                                                            out collectionDefinition))
                return (IWrappedCollection)ReflectionUtils.CreateGeneric(typeof(CollectionWrapper<>), new[]
                    {
                        ReflectionUtils.GetCollectionItemType(collectionDefinition)
                    }, (t, a) => t.GetConstructor(new[] { collectionDefinition }).Invoke(new[] { list }), new[] { list });
            if (list is IList)
            {
                return new CollectionWrapper<object>((IList)list);
            }
            throw new ArgumentException(
                StringUtils.FormatWith("Can not create ListWrapper for type {0}.", CultureInfo.InvariantCulture,
                                       list.GetType()), "list");
        }

        public static IWrappedDictionary CreateDictionaryWrapper(object dictionary)
        {
            ValidationUtils.ArgumentNotNull(dictionary, "dictionary");
            Type dictionaryDefinition;
            if (ReflectionUtils.ImplementsGenericDefinition(dictionary.GetType(), typeof(IDictionary<,>),
                                                            out dictionaryDefinition))
                return
                    (IWrappedDictionary)
                    ReflectionUtils.CreateGeneric(typeof(DictionaryWrapper<,>), new[]
                        {
                            ReflectionUtils.GetDictionaryKeyType(dictionaryDefinition),
                            ReflectionUtils.GetDictionaryValueType(dictionaryDefinition)
                        }, (t, a) => t.GetConstructor(new[] { dictionaryDefinition }).Invoke(new[] { dictionary }),
                                                  new[] { dictionary });
            if (dictionary is IDictionary)
            {
                return new DictionaryWrapper<object, object>((IDictionary)dictionary);
            }
            throw new ArgumentException(
                StringUtils.FormatWith("Can not create DictionaryWrapper for type {0}.", CultureInfo.InvariantCulture,
                                       dictionary.GetType()), "dictionary");
        }

        public static IList CreateList(Type listType, out bool isReadOnlyOrFixedSize)
        {
            ValidationUtils.ArgumentNotNull(listType, "listType");
            isReadOnlyOrFixedSize = false;
            IList list1;
            if (listType.IsArray)
            {
                list1 = new List<object>();
                isReadOnlyOrFixedSize = true;
            }
            else
            {
                Type implementingType;
                if (ReflectionUtils.InheritsGenericDefinition(listType, typeof(ReadOnlyCollection<>),
                                                              out implementingType))
                {
                    Type listType1 = implementingType.GetGenericArguments()[0];
                    Type type = ReflectionUtils.MakeGenericType(typeof(IEnumerable<>), new[] { listType1 });
                    bool flag = false;
                    foreach (MethodBase methodBase in listType.GetConstructors())
                    {
                        IList<ParameterInfo> list2 = methodBase.GetParameters();
                        if (list2.Count == 1 && type.IsAssignableFrom(list2[0].ParameterType))
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag)
                        throw new Exception(
                            StringUtils.FormatWith(
                                "Read-only type {0} does not have a public constructor that takes a type that implements {1}.",
                                CultureInfo.InvariantCulture, listType, type));
                    list1 = CreateGenericList(listType1);
                    isReadOnlyOrFixedSize = true;
                }
                else
                {
                    list1 = !typeof(IList).IsAssignableFrom(listType) ? (!ReflectionUtils.ImplementsGenericDefinition(listType, typeof(ICollection<>)) ? null : (!ReflectionUtils.IsInstantiatableType(listType) ? null : CreateCollectionWrapper(Activator.CreateInstance(listType)))) : (!ReflectionUtils.IsInstantiatableType(listType) ? (!(listType == typeof(IList)) ? null : new List<object>()) : (IList)Activator.CreateInstance(listType));
                }
            }
            if (list1 == null)
            {
                throw new InvalidOperationException(StringUtils.FormatWith("Cannot create and populate list type {0}.", CultureInfo.InvariantCulture, listType));
            }
            return list1;
        }

        public static Array ToArray(Array initial, Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            Array instance = Array.CreateInstance(type, initial.Length);
            Array.Copy(initial, 0, instance, 0, initial.Length);
            return instance;
        }

        public static bool AddDistinct<T>(this IList<T> list, T value)
        {
            return AddDistinct(list, value, EqualityComparer<T>.Default);
        }

        public static bool AddDistinct<T>(this IList<T> list, T value, IEqualityComparer<T> comparer)
        {
            if (ContainsValue(list, value, comparer))
            {
                return false;
            }
            list.Add(value);
            return true;
        }

        public static bool ContainsValue<TSource>(this IEnumerable<TSource> source, TSource value, IEqualityComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                comparer = EqualityComparer<TSource>.Default;
            }
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            foreach (TSource x in source)
            {
                if (comparer.Equals(x, value))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool AddRangeDistinct<T>(this IList<T> list, IEnumerable<T> values, IEqualityComparer<T> comparer)
        {
            bool flag = true;
            foreach (T obj in values)
            {
                if (!AddDistinct(list, obj, comparer))
                {
                    flag = false;
                }
            }
            return flag;
        }

        public static int IndexOf<T>(this IEnumerable<T> collection, Func<T, bool> predicate)
        {
            int num = 0;
            foreach (T obj in collection)
            {
                if (predicate(obj))
                {
                    return num;
                }
                ++num;
            }
            return -1;
        }

        public static int IndexOf<TSource>(this IEnumerable<TSource> list, TSource value, IEqualityComparer<TSource> comparer)
        {
            int num = 0;
            foreach (TSource x in list)
            {
                if (comparer.Equals(x, value))
                {
                    return num;
                }
                ++num;
            }
            return -1;
        }
    }
}
