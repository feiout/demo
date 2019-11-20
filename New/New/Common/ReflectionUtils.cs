using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Text;

namespace New.Common
{
    public static class ReflectionUtils
    {
        public static readonly Type[] EmptyTypes = Type.EmptyTypes;

        static ReflectionUtils()
        {
        }

        public static ICustomAttributeProvider GetCustomAttributeProvider(this object o)
        {
            return (ICustomAttributeProvider)o;
        }

        public static bool IsVirtual(this PropertyInfo propertyInfo)
        {
            ValidationUtils.ArgumentNotNull(propertyInfo, "propertyInfo");
            MethodInfo getMethod = propertyInfo.GetGetMethod();
            if (getMethod != null && getMethod.IsVirtual)
                return true;
            MethodInfo setMethod = propertyInfo.GetSetMethod();
            return setMethod != null && setMethod.IsVirtual;
        }

        public static Type GetObjectType(object v)
        {
            return v == null ? null : v.GetType();
        }

        public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat)
        {
            return GetTypeName(t, assemblyFormat, null);
        }

        public static string GetTypeName(Type t, FormatterAssemblyStyle assemblyFormat, SerializationBinder binder)
        {
            string fullyQualifiedTypeName;
            if (binder != null)
            {
                string assemblyName;
                string typeName;
                binder.BindToName(t, out assemblyName, out typeName);
                fullyQualifiedTypeName = typeName + (assemblyName == null ? "" : ", " + assemblyName);
            }
            else
                fullyQualifiedTypeName = t.AssemblyQualifiedName;
            switch (assemblyFormat)
            {
                case FormatterAssemblyStyle.Simple:
                    return RemoveAssemblyDetails(fullyQualifiedTypeName);
                case FormatterAssemblyStyle.Full:
                    return fullyQualifiedTypeName;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private static string RemoveAssemblyDetails(string fullyQualifiedTypeName)
        {
            StringBuilder stringBuilder = new StringBuilder();
            bool flag1 = false;
            bool flag2 = false;
            for (int index = 0; index < fullyQualifiedTypeName.Length; ++index)
            {
                char ch = fullyQualifiedTypeName[index];
                switch (ch)
                {
                    case ',':
                        if (!flag1)
                        {
                            flag1 = true;
                            stringBuilder.Append(ch);
                            break;
                        }
                        flag2 = true;
                        break;
                    case '[':
                        flag1 = false;
                        flag2 = false;
                        stringBuilder.Append(ch);
                        break;
                    case ']':
                        flag1 = false;
                        flag2 = false;
                        stringBuilder.Append(ch);
                        break;
                    default:
                        if (!flag2)
                        {
                            stringBuilder.Append(ch);
                        }
                        break;
                }
            }
            return (stringBuilder).ToString();
        }

        public static bool IsInstantiatableType(Type t)
        {
            ValidationUtils.ArgumentNotNull(t, "t");
            return !TypeExtensions.IsAbstract(t) && !TypeExtensions.IsInterface(t) && (!t.IsArray && !TypeExtensions.IsGenericTypeDefinition(t)) && (!(t == typeof(void)) && HasDefaultConstructor(t));
        }

        public static bool HasDefaultConstructor(Type t)
        {
            return HasDefaultConstructor(t, false);
        }

        public static bool HasDefaultConstructor(Type t, bool nonPublic)
        {
            ValidationUtils.ArgumentNotNull(t, "t");
            return TypeExtensions.IsValueType(t) || GetDefaultConstructor(t, nonPublic) != null;
        }

        public static ConstructorInfo GetDefaultConstructor(Type t)
        {
            return GetDefaultConstructor(t, false);
        }

        public static ConstructorInfo GetDefaultConstructor(Type t, bool nonPublic)
        {
            BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public;
            if (nonPublic)
                bindingAttr |= BindingFlags.NonPublic;
            return Enumerable.SingleOrDefault(t.GetConstructors(bindingAttr), c => !Enumerable.Any(c.GetParameters()));
        }

        public static bool IsNullable(Type t)
        {
            ValidationUtils.ArgumentNotNull(t, "t");
            return !TypeExtensions.IsValueType(t) || IsNullableType(t);
        }

        public static bool IsNullableType(Type t)
        {
            ValidationUtils.ArgumentNotNull(t, "t");
            return TypeExtensions.IsGenericType(t) && t.GetGenericTypeDefinition() == typeof (Nullable<>);
        }

        public static Type EnsureNotNullableType(Type t)
        {
            return !IsNullableType(t) ? t : Nullable.GetUnderlyingType(t);
        }

        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition)
        {
            Type implementingType;
            return ImplementsGenericDefinition(type, genericInterfaceDefinition, out implementingType);
        }

        public static bool ImplementsGenericDefinition(Type type, Type genericInterfaceDefinition, out Type implementingType)
        {
            ValidationUtils.ArgumentNotNull(type, "type");
            ValidationUtils.ArgumentNotNull(genericInterfaceDefinition, "genericInterfaceDefinition");
            if (!TypeExtensions.IsInterface(genericInterfaceDefinition) || !TypeExtensions.IsGenericTypeDefinition(genericInterfaceDefinition))
                throw new ArgumentNullException(StringUtils.FormatWith("'{0}' is not a generic interface definition.", CultureInfo.InvariantCulture, genericInterfaceDefinition));
            if (TypeExtensions.IsInterface(type) && TypeExtensions.IsGenericType(type))
            {
                Type genericTypeDefinition = type.GetGenericTypeDefinition();
                if (genericInterfaceDefinition == genericTypeDefinition)
                {
                    implementingType = type;
                    return true;
                }
            }
            foreach (Type type1 in type.GetInterfaces())
            {
                if (TypeExtensions.IsGenericType(type1))
                {
                    Type genericTypeDefinition = type1.GetGenericTypeDefinition();
                    if (genericInterfaceDefinition == genericTypeDefinition)
                    {
                        implementingType = type1;
                        return true;
                    }
                }
            }
            implementingType = null;
            return false;
        }

        public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition)
        {
            Type implementingType;
            return InheritsGenericDefinition(type, genericClassDefinition, out implementingType);
        }

        public static bool InheritsGenericDefinition(Type type, Type genericClassDefinition, out Type implementingType)
        {
            ValidationUtils.ArgumentNotNull(type, "type");
            ValidationUtils.ArgumentNotNull(genericClassDefinition, "genericClassDefinition");
            if (!TypeExtensions.IsClass(genericClassDefinition) || !TypeExtensions.IsGenericTypeDefinition(genericClassDefinition))
                throw new ArgumentNullException(StringUtils.FormatWith("'{0}' is not a generic class definition.", CultureInfo.InvariantCulture, genericClassDefinition));
            return InheritsGenericDefinitionInternal(type, genericClassDefinition, out implementingType);
        }

        private static bool InheritsGenericDefinitionInternal(Type currentType, Type genericClassDefinition, out Type implementingType)
        {
            if (TypeExtensions.IsGenericType(currentType))
            {
                Type genericTypeDefinition = currentType.GetGenericTypeDefinition();
                if (genericClassDefinition == genericTypeDefinition)
                {
                    implementingType = currentType;
                    return true;
                }
            }
            if (!(TypeExtensions.BaseType(currentType) == null))
                return InheritsGenericDefinitionInternal(TypeExtensions.BaseType(currentType), genericClassDefinition, out implementingType);
            implementingType = null;
            return false;
        }

        public static Type GetCollectionItemType(Type type)
        {
            ValidationUtils.ArgumentNotNull(type, "type");
            if (type.IsArray)
                return type.GetElementType();
            Type implementingType;
            if (ImplementsGenericDefinition(type, typeof(IEnumerable<>), out implementingType))
            {
                if (TypeExtensions.IsGenericTypeDefinition(implementingType))
                    throw new Exception(StringUtils.FormatWith("Type {0} is not a collection.", CultureInfo.InvariantCulture, type));
                return implementingType.GetGenericArguments()[0];
            }
            if (typeof(IEnumerable).IsAssignableFrom(type))
                return null;
            throw new Exception(StringUtils.FormatWith("Type {0} is not a collection.", CultureInfo.InvariantCulture, type));
        }

        public static void GetDictionaryKeyValueTypes(Type dictionaryType, out Type keyType, out Type valueType)
        {
            ValidationUtils.ArgumentNotNull(dictionaryType, "type");
            Type implementingType;
            if (ImplementsGenericDefinition(dictionaryType, typeof(IDictionary<,>), out implementingType))
            {
                if (TypeExtensions.IsGenericTypeDefinition(implementingType))
                    throw new Exception(StringUtils.FormatWith("Type {0} is not a dictionary.", CultureInfo.InvariantCulture, dictionaryType));
                Type[] genericArguments = implementingType.GetGenericArguments();
                keyType = genericArguments[0];
                valueType = genericArguments[1];
            }
            else
            {
                if (!typeof(IDictionary).IsAssignableFrom(dictionaryType))
                    throw new Exception(StringUtils.FormatWith("Type {0} is not a dictionary.", CultureInfo.InvariantCulture, dictionaryType));
                keyType = null;
                valueType = null;
            }
        }

        public static Type GetDictionaryValueType(Type dictionaryType)
        {
            Type keyType;
            Type valueType;
            GetDictionaryKeyValueTypes(dictionaryType, out keyType, out valueType);
            return valueType;
        }

        public static Type GetDictionaryKeyType(Type dictionaryType)
        {
            Type keyType;
            Type valueType;
            GetDictionaryKeyValueTypes(dictionaryType, out keyType, out valueType);
            return keyType;
        }

        public static Type GetMemberUnderlyingType(MemberInfo member)
        {
            ValidationUtils.ArgumentNotNull(member, "member");
            switch (TypeExtensions.MemberType(member))
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;
                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;
                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;
                default:
                    throw new ArgumentException("MemberInfo must be of type FieldInfo, PropertyInfo or EventInfo", "member");
            }
        }

        public static bool IsIndexedProperty(MemberInfo member)
        {
            ValidationUtils.ArgumentNotNull(member, "member");
            PropertyInfo property = member as PropertyInfo;
            return property != null && IsIndexedProperty(property);
        }

        public static bool IsIndexedProperty(PropertyInfo property)
        {
            ValidationUtils.ArgumentNotNull(property, "property");
            return property.GetIndexParameters().Length > 0;
        }

        public static object GetMemberValue(MemberInfo member, object target)
        {
            ValidationUtils.ArgumentNotNull(member, "member");
            ValidationUtils.ArgumentNotNull(target, "target");
            switch (TypeExtensions.MemberType(member))
            {
                case MemberTypes.Field:
                    return ((FieldInfo)member).GetValue(target);
                case MemberTypes.Property:
                    try
                    {
                        return ((PropertyInfo)member).GetValue(target, null);
                    }
                    catch (TargetParameterCountException ex)
                    {
                        throw new ArgumentException(StringUtils.FormatWith("MemberInfo '{0}' has index parameters", CultureInfo.InvariantCulture, member.Name), ex);
                    }
                default:
                    throw new ArgumentException(StringUtils.FormatWith("MemberInfo '{0}' is not of type FieldInfo or PropertyInfo", CultureInfo.InvariantCulture, CultureInfo.InvariantCulture, member.Name), "member");
            }
        }

        public static void SetMemberValue(MemberInfo member, object target, object value)
        {
            ValidationUtils.ArgumentNotNull(member, "member");
            ValidationUtils.ArgumentNotNull(target, "target");
            switch (TypeExtensions.MemberType(member))
            {
                case MemberTypes.Field:
                    ((FieldInfo)member).SetValue(target, value);
                    break;
                case MemberTypes.Property:
                    ((PropertyInfo)member).SetValue(target, value, null);
                    break;
                default:
                    throw new ArgumentException(StringUtils.FormatWith("MemberInfo '{0}' must be of type FieldInfo or PropertyInfo", CultureInfo.InvariantCulture, member.Name), "member");
            }
        }

        public static bool CanReadMemberValue(MemberInfo member, bool nonPublic)
        {
            switch (TypeExtensions.MemberType(member))
            {
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)member;
                    return nonPublic || fieldInfo.IsPublic;
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)member;
                    if (!propertyInfo.CanRead)
                        return false;
                    return nonPublic || propertyInfo.GetGetMethod(nonPublic) != null;
                default:
                    return false;
            }
        }

        public static bool CanSetMemberValue(MemberInfo member, bool nonPublic, bool canSetReadOnly)
        {
            switch (TypeExtensions.MemberType(member))
            {
                case MemberTypes.Field:
                    FieldInfo fieldInfo = (FieldInfo)member;
                    return (!fieldInfo.IsInitOnly || canSetReadOnly) && (nonPublic || fieldInfo.IsPublic);
                case MemberTypes.Property:
                    PropertyInfo propertyInfo = (PropertyInfo)member;
                    if (!propertyInfo.CanWrite)
                        return false;
                    return nonPublic || propertyInfo.GetSetMethod(nonPublic) != null;
                default:
                    return false;
            }
        }

        public static List<MemberInfo> GetFieldsAndProperties(Type type, BindingFlags bindingAttr)
        {
            List<MemberInfo> list1 = new List<MemberInfo>();
            list1.AddRange(GetFields(type, bindingAttr));
            list1.AddRange(GetProperties(type, bindingAttr));
            List<MemberInfo> list2 = new List<MemberInfo>(list1.Count);
            foreach (IGrouping<string, MemberInfo> grouping in Enumerable.GroupBy(list1, m => m.Name))
            {
                int num = Enumerable.Count(grouping);
                IList<MemberInfo> list3 = Enumerable.ToList(grouping);
                if (num == 1)
                {
                    list2.Add(Enumerable.First(list3));
                }
                else
                {
                    IEnumerable<MemberInfo> collection = Enumerable.Where(list3, m =>
                        { return !IsOverridenGenericMember(m, bindingAttr) || m.Name == "Item"; });
                    list2.AddRange(collection);
                }
            }
            return list2;
        }

        private static bool IsOverridenGenericMember(MemberInfo memberInfo, BindingFlags bindingAttr)
        {
            switch (TypeExtensions.MemberType(memberInfo))
            {
                case MemberTypes.Field:
                case MemberTypes.Property:
                    Type declaringType = memberInfo.DeclaringType;
                    if (!TypeExtensions.IsGenericType(declaringType))
                        return false;
                    Type genericTypeDefinition = declaringType.GetGenericTypeDefinition();
                    if (genericTypeDefinition == null)
                        return false;
                    MemberInfo[] member = genericTypeDefinition.GetMember(memberInfo.Name, bindingAttr);
                    return member.Length != 0 && GetMemberUnderlyingType(member[0]).IsGenericParameter;
                default:
                    throw new ArgumentException("Member must be a field or property.");
            }
        }

        public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider) where T : Attribute
        {
            return GetAttribute<T>(attributeProvider, true);
        }

        public static T GetAttribute<T>(ICustomAttributeProvider attributeProvider, bool inherit) where T : Attribute
        {
            return Enumerable.SingleOrDefault(GetAttributes<T>(attributeProvider, inherit));
        }

        public static T[] GetAttributes<T>(ICustomAttributeProvider attributeProvider, bool inherit) where T : Attribute
        {
            ValidationUtils.ArgumentNotNull(attributeProvider, "attributeProvider");
            object obj = attributeProvider;
            if (obj is Type)
                return (T[])((Type)obj).GetCustomAttributes(typeof(T), inherit);
            if (obj is Assembly)
                return (T[])Attribute.GetCustomAttributes((Assembly)obj, typeof(T));
            if (obj is MemberInfo)
                return (T[])Attribute.GetCustomAttributes((MemberInfo)obj, typeof(T), inherit);
            if (obj is Module)
                return (T[])Attribute.GetCustomAttributes((Module)obj, typeof(T), inherit);
            if (obj is ParameterInfo)
                return (T[])Attribute.GetCustomAttributes((ParameterInfo)obj, typeof(T), inherit);
            return (T[])attributeProvider.GetCustomAttributes(typeof(T), inherit);
        }

        public static Type MakeGenericType(Type genericTypeDefinition, params Type[] innerTypes)
        {
            ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
            ValidationUtils.ArgumentNotNullOrEmpty(innerTypes, "innerTypes");
            ValidationUtils.ArgumentConditionTrue(TypeExtensions.IsGenericTypeDefinition(genericTypeDefinition), "genericTypeDefinition", StringUtils.FormatWith("Type {0} is not a generic type definition.", CultureInfo.InvariantCulture, genericTypeDefinition));
            return genericTypeDefinition.MakeGenericType(innerTypes);
        }

        public static object CreateGeneric(Type genericTypeDefinition, Type innerType, params object[] args)
        {
            return CreateGeneric(genericTypeDefinition, new []{innerType}, args);
        }

        public static object CreateGeneric(Type genericTypeDefinition, IList<Type> innerTypes, params object[] args)
        {
            return CreateGeneric(genericTypeDefinition, innerTypes, (t, a) => CreateInstance(t, Enumerable.ToArray(a)), args);
        }

        public static object CreateGeneric(Type genericTypeDefinition, IList<Type> innerTypes, Func<Type, IList<object>, object> instanceCreator, params object[] args)
        {
            ValidationUtils.ArgumentNotNull(genericTypeDefinition, "genericTypeDefinition");
            ValidationUtils.ArgumentNotNullOrEmpty(innerTypes, "innerTypes");
            ValidationUtils.ArgumentNotNull(instanceCreator, "createInstance");
            Type type = MakeGenericType(genericTypeDefinition, Enumerable.ToArray(innerTypes));
            return instanceCreator(type, args);
        }

        public static object CreateInstance(Type type, params object[] args)
        {
            ValidationUtils.ArgumentNotNull(type, "type");
            return Activator.CreateInstance(type, args);
        }

        public static void SplitFullyQualifiedTypeName(string fullyQualifiedTypeName, out string typeName, out string assemblyName)
        {
            int? assemblyDelimiterIndex = GetAssemblyDelimiterIndex(fullyQualifiedTypeName);
            if (assemblyDelimiterIndex.HasValue)
            {
                typeName = fullyQualifiedTypeName.Substring(0, assemblyDelimiterIndex.Value).Trim();
                assemblyName = fullyQualifiedTypeName.Substring(assemblyDelimiterIndex.Value + 1, fullyQualifiedTypeName.Length - assemblyDelimiterIndex.Value - 1).Trim();
            }
            else
            {
                typeName = fullyQualifiedTypeName;
                assemblyName = null;
            }
        }

        private static int? GetAssemblyDelimiterIndex(string fullyQualifiedTypeName)
        {
            int num = 0;
            for (int index = 0; index < fullyQualifiedTypeName.Length; ++index)
            {
                switch (fullyQualifiedTypeName[index])
                {
                    case ',':
                        if (num == 0)
                            return index;
                        break;
                    case '[':
                        ++num;
                        break;
                    case ']':
                        --num;
                        break;
                }
            }
            return new int?();
        }

        public static MemberInfo GetMemberInfoFromType(Type targetType, MemberInfo memberInfo)
        {
            if (TypeExtensions.MemberType(memberInfo) != MemberTypes.Property)
                return Enumerable.SingleOrDefault(targetType.GetMember(memberInfo.Name, TypeExtensions.MemberType(memberInfo), BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic));
            PropertyInfo propertyInfo = (PropertyInfo)memberInfo;
            Type[] types = Enumerable.ToArray(Enumerable.Select(propertyInfo.GetIndexParameters(), p => p.ParameterType));
            return targetType.GetProperty(propertyInfo.Name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, propertyInfo.PropertyType, types, null);
        }

        public static IEnumerable<FieldInfo> GetFields(Type targetType, BindingFlags bindingAttr)
        {
            ValidationUtils.ArgumentNotNull(targetType, "targetType");
            List<MemberInfo> list = new List<MemberInfo>(targetType.GetFields(bindingAttr));
            GetChildPrivateFields(list, targetType, bindingAttr);
            return Enumerable.Cast<FieldInfo>(list);
        }

        private static void GetChildPrivateFields(IList<MemberInfo> initialFields, Type targetType, BindingFlags bindingAttr)
        {
            if ((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default)
                return;
            BindingFlags bindingAttr1 = RemoveFlag(bindingAttr, BindingFlags.Public);
            while ((targetType = TypeExtensions.BaseType(targetType)) != null)
            {
                IEnumerable<MemberInfo> collection = Enumerable.Where(targetType.GetFields(bindingAttr1), f => f.IsPrivate);
                CollectionUtils.AddRange(initialFields, collection);
            }
        }

        public static IEnumerable<PropertyInfo> GetProperties(Type targetType, BindingFlags bindingAttr)
        {
            ValidationUtils.ArgumentNotNull(targetType, "targetType");
            List<PropertyInfo> list = new List<PropertyInfo>(targetType.GetProperties(bindingAttr));
            GetChildPrivateProperties(list, targetType, bindingAttr);
            for (int index = 0; index < list.Count; ++index)
            {
                PropertyInfo propertyInfo1 = list[index];
                if (propertyInfo1.DeclaringType != targetType)
                {
                    PropertyInfo propertyInfo2 = (PropertyInfo)GetMemberInfoFromType(propertyInfo1.DeclaringType, propertyInfo1);
                    list[index] = propertyInfo2;
                }
            }
            return list;
        }

        public static BindingFlags RemoveFlag(this BindingFlags bindingAttr, BindingFlags flag)
        {
            return (bindingAttr & flag) != flag ? bindingAttr : bindingAttr ^ flag;
        }

        private static void GetChildPrivateProperties(IList<PropertyInfo> initialProperties, Type targetType, BindingFlags bindingAttr)
        {
            if ((bindingAttr & BindingFlags.NonPublic) == BindingFlags.Default)
                return;
            BindingFlags bindingAttr1 = RemoveFlag(bindingAttr, BindingFlags.Public);
            while ((targetType = TypeExtensions.BaseType(targetType)) != null)
            {
                foreach (PropertyInfo propertyInfo in targetType.GetProperties(bindingAttr1))
                {
                    PropertyInfo nonPublicProperty = propertyInfo;
                    int index = CollectionUtils.IndexOf(initialProperties, p => p.Name == nonPublicProperty.Name);
                    if (index == -1)
                        initialProperties.Add(nonPublicProperty);
                    else
                        initialProperties[index] = nonPublicProperty;
                }
            }
        }

        public static bool IsMethodOverridden(Type currentType, Type methodDeclaringType, string method)
        {
            return Enumerable.Any(currentType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic), info =>
                {
                    if (info.Name == method && info.DeclaringType != methodDeclaringType)
                        return info.GetBaseDefinition().DeclaringType == methodDeclaringType;
                    return false;
                });
        }
    }
}
