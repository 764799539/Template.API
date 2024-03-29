﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Template.NuGet
{
    public static class EnumHelper
    {
        private static Dictionary<Type, Dictionary<object, string>> s_Cache = new Dictionary<Type, Dictionary<object, string>>();
        private static object s_SyncObj = new object();

        /// <summary>
        /// 获取枚举值中文描述
        /// </summary>
        /// <param name="Value">(枚举类)枚举值</param>
        /// <returns></returns>
        public static string GetDescription(this Enum Value) => GetDescription(Value, Value.GetType());

        /// <summary>
        /// 获取枚举值中文描述
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <param name="enumType">枚举类</param>
        /// <returns></returns>
        private static string GetDescription(object enumValue, Type enumType)
        {
            if (GetDescriptions(enumType).TryGetValue(enumValue, out string str) && (str != null))
                return str;
            return string.Empty;
        }

        public static Dictionary<TEnum, string> GetDescriptions<TEnum>() where TEnum : struct
        {
            Dictionary<object, string> descriptions = GetDescriptions(typeof(TEnum));
            Dictionary<TEnum, string> dictionary = new Dictionary<TEnum, string>(descriptions.Count * 2);
            foreach (KeyValuePair<object, string> pair in descriptions)
            {
                dictionary.Add((TEnum)pair.Key, pair.Value);
            }
            return dictionary;
        }

        private static Dictionary<object, string> GetDescriptions(Type enumType)
        {
            if (!enumType.IsEnum && !IsGenericEnum(enumType))
            {
                throw new ApplicationException("The generic type 'TEnum' must be enum or Nullable<enum>.");
            }
            enumType = GetRealEnum(enumType);
            if (s_Cache.TryGetValue(enumType, out Dictionary<object, string> dictionary))
            {
                return dictionary;
            } 
            object obj2 = s_SyncObj;
            lock (obj2)
            {
                if (s_Cache.TryGetValue(enumType, out dictionary))
                {
                    return dictionary;
                }
                FieldInfo[] fields = enumType.GetFields((BindingFlags.Public) | (BindingFlags.Static));
                Dictionary<object, string> dictionary2 = new Dictionary<object, string>(fields.Length * 2);
                foreach (FieldInfo info in fields)
                {
                    object[] customAttributes = info.GetCustomAttributes(typeof(DisplayAttribute), false);
                    if (((customAttributes == null) || (customAttributes.Length == 0)) || (customAttributes[0] as DisplayAttribute).Display)
                    {
                        object[] objArray2 = info.GetCustomAttributes(typeof(DescriptionAttribute), false);
                        string description = string.Empty;
                        if ((objArray2 != null) && (objArray2.Length != 0))
                        {
                            DescriptionAttribute attribute = objArray2[0] as DescriptionAttribute;
                            if ((attribute != null) && (attribute.Description != null))
                            {
                                description = attribute.Description;
                            }
                        }
                        object key = info.GetValue(null);
                        dictionary2.Add(key, description);
                    }
                }
                s_Cache.Add(enumType, dictionary2);
                return dictionary2;
            }
        }

        public static string GetEnumDescription(Type enumType, object val)
        {
            string name = Enum.GetName(enumType, val);
            if (string.IsNullOrEmpty(name))
            {
                return string.Empty;
            }
            object[] customAttributes = enumType.GetField(name).GetCustomAttributes((Type)typeof(DescriptionAttribute), true);
            if (customAttributes.Length != 0)
            {
                DescriptionAttribute attribute = customAttributes[0] as DescriptionAttribute;
                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            return name;
        }

        public static IList<EnumInfo> GetEnumList<T>()
        {
            IList<EnumInfo> list = new List<EnumInfo>();
            EnumInfo item = null;
            Type enumType = typeof(T);
            foreach (int num in Enum.GetValues(typeof(T)))
            {
                item = new EnumInfo
                {
                    Text = GetEnumDescription(enumType, num),
                    Value = GetModel<T>(num).ToString(),
                    NumValue = num
                };
                list.Add(item);
            }
            return list;
        }

        public static List<KeyValuePair<TEnum?, string>> GetKeyValuePairs<TEnum>(EnumAppendItemType appendType, params string[] customApplyDesc) where TEnum : struct
        {
            List<KeyValuePair<TEnum?, string>> list = new List<KeyValuePair<TEnum?, string>>();
            Type type = typeof(TEnum);
            if (type.IsEnum || IsGenericEnum(type))
            {
                Dictionary<TEnum, string> descriptions = GetDescriptions<TEnum>();
                if ((descriptions != null) && (descriptions.Count > 0))
                {
                    foreach (TEnum local in descriptions.Keys)
                    {
                        list.Add(new KeyValuePair<TEnum?, string>(new TEnum?(local), descriptions[local]));
                    }
                }
                if (appendType == EnumAppendItemType.None)
                {
                    return list;
                }
                if (((customApplyDesc != null) && (customApplyDesc.Length != 0)) && !string.IsNullOrEmpty(customApplyDesc[0]))
                {
                    KeyValuePair<TEnum?, string> item = new KeyValuePair<TEnum?, string>(null, customApplyDesc[0]);
                    list.Insert(0, item);
                    return list;
                }
                string description = appendType.GetDescription();
                if (!string.IsNullOrEmpty(description))
                {
                    KeyValuePair<TEnum?, string> item = new KeyValuePair<TEnum?, string>(null, description);
                    list.Insert(0, item);
                }
            }
            return list;
        }

        public static List<object> GetKeyValuePairs2<TEnum>()
        {
            List<object> list = new List<object>();
            Type type = typeof(TEnum);
            if (type.IsEnum || IsGenericEnum(type))
            {
                foreach (object obj2 in Enum.GetValues(type))
                {
                    list.Add(new
                    {
                        value = (int)obj2,
                        text = GetDescription(obj2, type)
                    });
                }
            }
            return list;
        }

        public static T GetModel<T>(int Value) => (T)Enum.Parse(typeof(T), Value.ToString(), true);

        public static T GetModel<T>(string value) =>
            ((T)Enum.Parse(typeof(T), value, true));

        private static Type GetRealEnum(Type type)
        {
            Type type2 = type;
            while (IsGenericEnum(type2))
            {
                type2 = type.GetGenericArguments()[0];
            }
            return type2;
        }

        private static bool IsGenericEnum(Type type) =>
            (((type.IsGenericType && (type.GetGenericTypeDefinition() == typeof(Nullable<>))) && ((type.GetGenericArguments() != null) && (type.GetGenericArguments().Length == 1))) && type.GetGenericArguments()[0].IsEnum);
    }

}
