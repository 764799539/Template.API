using Newtonsoft.Json;
using ServiceStack;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Template.NuGet
{
    /// <summary>
    /// Redis帮助类
    /// </summary>
    public static class RedisHelper
    {
        private static IRedis _redisHelper;
        /// <summary>
        /// Redis设置
        /// </summary>
        private static RedisSettings _RedisSettings = RedisSettings.Config;

        static RedisHelper()
        {
            string mode = _RedisSettings.Mode;
            string type = string.Empty;
            RedisProviders redisProviders = _RedisSettings.RedisProviders;
            if ((redisProviders.RedisSentinelProvider != null) && string.Equals(redisProviders.RedisSentinelProvider.Name, mode, StringComparison.CurrentCultureIgnoreCase))
            {
                type = redisProviders.RedisSentinelProvider.Type;
            }
            else if ((redisProviders.RedisGeneralProvider != null) && string.Equals(redisProviders.RedisGeneralProvider.Name, mode, StringComparison.CurrentCultureIgnoreCase))
            {
                type = redisProviders.RedisGeneralProvider.Type;
            }
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new RedisException("找不到Redis模式节点, 请选择模式节点。");
            }
            _redisHelper = Activator.CreateInstance(Type.GetType(type)) as IRedis;
            _redisHelper.Start();
        }

        public static bool AddSortSet<T>(string key, T obj, double score)
        {
            key = GetRedisKey(key);
            string sortedSetValue = GetSortedSetValue(obj);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                return client.AddItemToSortedSet(key, sortedSetValue, score);
            }
        }

        public static long AddSortSetList<T>(string key, List<T> list, Func<T, double> generateScore)
        {
            int num = 0;
            foreach (T local in list)
            {
                if (AddSortSet<T>(key, local, generateScore(local)))
                {
                    num++;
                }
            }
            return num;
        }

        public static long AddSortSetList<T>(string key, List<T> list, Func<T, object> generateValue, Func<T, double> generateScore)
        {
            key = GetRedisKey(key);
            int num = 0;
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                foreach (T local in list)
                {
                    string sortedSetValue = GetSortedSetValue(generateValue(local));
                    double num2 = generateScore(local);
                    if (client.AddItemToSortedSet(key, sortedSetValue, num2))
                    {
                        num++;
                    }
                }
                return num;
            }
        }

        public static long AppendString(string key, string value)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                return client.AppendToValue(key, value);
            }
        }

        public static long DeleteHash(string key, List<string> hashFields)
        {
            if (hashFields == null)
            {
                return 0L;
            }
            int deleteCount = 0;
            hashFields.ForEach(delegate (string field) {
                if (DeleteHash(key, field))
                {
                    int num = deleteCount;
                    deleteCount = num + 1;
                }
            });
            return (long)deleteCount;
        }

        public static bool DeleteHash(string key, string hashField)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                return client.RemoveEntryFromHash(key, hashField);
            }
        }

        public static long DeleteKey(List<string> listKey)
        {
            if (listKey == null)
            {
                return 0L;
            }
            List<string> redisKeyList = new List<string>();
            listKey.ForEach(delegate (string key) {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    redisKeyList.Add(GetRedisKey(key));
                }
            });
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                client.RemoveAll((IEnumerable<string>)redisKeyList);
                return redisKeyList.Count;
            }
        }

        public static bool DeleteKey(string key)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                return client.Remove(key);
            }
        }

        public static bool ExistsKey(string key)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                return client.ContainsKey(key);
            }
        }

        public static List<T> GetAllHash<T>(string key)
        {
            key = GetRedisKey(key);
            List<T> list = new List<T>();
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                foreach (string str in client.GetHashValues(key))
                {
                    if (!string.IsNullOrWhiteSpace(str))
                    {
                        list.Add(JsonConvert.DeserializeObject<T>(str));
                    }
                }
                return list;
            }
        }

        public static List<string> GetCurrentDomainKeys() =>
            GetKeys(string.Empty, RedisMatch.RightLike);

        public static T GetHash<T>(string key, string hashField)
        {
            key = GetRedisKey(key);
            if (!string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(hashField))
            {
                using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
                {
                    string valueFromHash = client.GetValueFromHash(key, hashField);
                    if (!string.IsNullOrWhiteSpace(valueFromHash))
                    {
                        return JsonConvert.DeserializeObject<T>(valueFromHash);
                    }
                }
            }
            return default(T);
        }

        public static List<string> GetHashFields(string key)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                return client.GetHashKeys(key);
            }
        }

        public static List<T> GetHashList<T>(string key, List<string> hashFields)
        {
            key = GetRedisKey(key);
            List<T> list = new List<T>();
            if (string.IsNullOrWhiteSpace(key) || (hashFields.Count <= 0))
            {
                return list;
            }
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                foreach (string str in hashFields)
                {
                    string valueFromHash = client.GetValueFromHash(key, str);
                    if (!string.IsNullOrWhiteSpace(valueFromHash))
                    {
                        list.Add(JsonConvert.DeserializeObject<T>(valueFromHash));
                    }
                }
                return list;
            }
        }

        public static List<string> GetKeys(string key, RedisMatch match = 0)
        {
            key = GetRedisKey(key);
            string str = string.Empty;
            switch (match)
            {
                case RedisMatch.Like:
                    str = $"*{key}*";
                    break;

                case RedisMatch.LeftLike:
                    str = $"*{key}";
                    break;

                case RedisMatch.RightLike:
                    str = $"{key}*";
                    break;

                default:
                    str = key;
                    break;
            }
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                string prefixKey = GetRedisKey(string.Empty);
                return Enumerable.ToList<string>(Enumerable.Select<string, string>(client.GetKeysByPattern(str), delegate (string s) {
                    return StringExtensions.RightPart(s, prefixKey);
                }));
            }
        }

        private static string GetRedisKey(string key)
        {
            if (!string.IsNullOrWhiteSpace(_RedisSettings.Domain))
            {
                string str = $"{_RedisSettings.Domain}_";
                if (key != null)
                {
                    key = $"{str}{key}";
                }
            }
            return key;
        }

        private static string GetSortedSetValue(object value)
        {
            Type type = value.GetType();
            if ((type.IsPrimitive || (type == typeof(string))) || (type == typeof(decimal)))
            {
                return value.ToString();
            }
            return JsonConvert.SerializeObject(value);
        }

        public static List<string> GetString(List<string> listKey)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < listKey.Count; i++)
            {
                list.Add(GetRedisKey(listKey[i]));
            }
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                return client.GetValues<string>(list);
            }
        }

        public static string GetString(string key)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                return client.GetValue(key);
            }
        }

        public static T GetString<T>(string key)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                string str = client.GetValue(key);
                if (str == null)
                {
                    return default(T);
                }
                return JsonConvert.DeserializeObject<T>(str);
            }
        }

        public static Dictionary<string, string> GetStringKeyValues(List<string> keys)
        {
            List<string> list = new List<string>();
            for (int i = 0; i < keys.Count; i++)
            {
                list.Add(GetRedisKey(keys[i]));
            }
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                Dictionary<string, string> dictionary = new Dictionary<string, string>();
                string redisKey = GetRedisKey(string.Empty);
                foreach (KeyValuePair<string, string> pair in client.GetValuesMap(list))
                {
                    string key = StringExtensions.RightPart(pair.Key, redisKey);
                    string str3 = pair.Value;
                    dictionary.Add(key, str3);
                }
                return dictionary;
            }
        }

        public static List<T> RangeSortedSetByRank<T>(string key, int start, int stop, bool isAscending = true)
        {
            key = GetRedisKey(key);
            List<string> list = new List<string>();
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                if (isAscending)
                {
                    list.AddRange((IEnumerable<string>)client.GetRangeFromSortedSet(key, start, stop));
                }
                else
                {
                    list.AddRange((IEnumerable<string>)client.GetRangeFromSortedSetDesc(key, start, stop));
                }
            }
            List<T> list2 = new List<T>();
            Type type = typeof(T);
            bool flag = (type.IsPrimitive || (type == typeof(string))) || (type == typeof(decimal));
            foreach (string str in list)
            {
                if (flag)
                {
                    list2.Add((T)Convert.ChangeType(str, type));
                }
                else if (!string.IsNullOrWhiteSpace(str))
                {
                    list2.Add(JsonConvert.DeserializeObject<T>(str));
                }
            }
            return list2;
        }

        public static List<T> RangeSortedSetByScore<T>(string key, long start, long stop, bool isAscending = true)
        {
            key = GetRedisKey(key);
            List<string> list = new List<string>();
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                if (isAscending)
                {
                    list.AddRange((IEnumerable<string>)client.GetRangeFromSortedSetByLowestScore(key, start, stop));
                }
                else
                {
                    list.AddRange((IEnumerable<string>)client.GetRangeFromSortedSetByHighestScore(key, start, stop));
                }
            }
            List<T> list2 = new List<T>();
            Type type = typeof(T);
            bool flag = (type.IsPrimitive || (type == typeof(string))) || (type == typeof(decimal));
            foreach (string str in list)
            {
                if (flag)
                {
                    list2.Add((T)Convert.ChangeType(str, type));
                }
                else if (!string.IsNullOrWhiteSpace(str))
                {
                    list2.Add(JsonConvert.DeserializeObject<T>(str));
                }
            }
            return list2;
        }

        public static IDictionary<string, double> RangeSortedSetWithScoreByRank(string key, int start, int stop, bool isAscending = true)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                if (isAscending)
                {
                    return (IDictionary<string, double>)(client.GetRangeWithScoresFromSortedSet(key, start, stop) as Dictionary<string, double>);
                }
                return (IDictionary<string, double>)(client.GetRangeWithScoresFromSortedSetDesc(key, start, stop) as Dictionary<string, double>);
            }
        }

        public static IDictionary<string, double> RangeSortedSetWithScoreByScore(string key, long start, long stop, bool isAscending = true)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                if (isAscending)
                {
                    return client.GetRangeWithScoresFromSortedSetByLowestScore(key, start, stop);
                }
                return client.GetRangeWithScoresFromSortedSetByHighestScore(key, start, stop);
            }
        }

        public static void RemoveSortedSetByScore(string key, double score)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                client.RemoveRangeFromSortedSetByScore(key, score, score);
            }
        }

        public static void RemoveSortedSetByScore<T>(string key, List<T> list, Func<T, double> generateScore)
        {
            foreach (T local in list)
            {
                RemoveSortedSetByScore(key, generateScore(local));
            }
        }

        public static void RemoveSortedSetByScore(string key, double fromScore, double toScore)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                client.RemoveRangeFromSortedSetByScore(key, fromScore, toScore);
            }
        }

        public static void RemoveSortedSetByValue(string key, string value)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                client.RemoveItemFromSortedSet(key, value);
            }
        }

        public static void RemoveSortedSetByValue<T>(string key, List<T> list, Func<T, object> generateValue)
        {
            foreach (T local in list)
            {
                string sortedSetValue = GetSortedSetValue(generateValue(local));
                RemoveSortedSetByValue(key, sortedSetValue);
            }
        }

        public static bool RenameKey(string key, string newKey)
        {
            key = GetRedisKey(key);
            newKey = GetRedisKey(newKey);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                client.RenameKey(key, newKey);
                return true;
            }
        }

        public static void SetHash<T>(string key, List<T> list, Func<T, string> generateFieldName)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                foreach (T local in list)
                {
                    string str = JsonConvert.SerializeObject(local);
                    string str2 = generateFieldName(local);
                    client.SetEntryInHash(key, str2, str);
                }
            }
        }

        public static void SetHash(string key, string field, object value)
        {
            if (value != null)
            {
                key = GetRedisKey(key);
                string str = JsonConvert.SerializeObject(value);
                using (IRedisClient client = RedisClientManager.GetClient())
                {
                    client.SetEntryInHash(key, field, str);
                }
            }
        }

        public static bool SetString(ICollection<KeyValuePair<string, string>> keyValues)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> pair in keyValues)
            {
                string redisKey = GetRedisKey(pair.Key);
                dictionary.Add(redisKey, pair.Value);
            }
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                client.SetValues(dictionary);
                return true;
            }
        }

        public static bool SetString(string key, string value, TimeSpan? expiry = new TimeSpan?())
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetClient())
            {
                if (!expiry.HasValue || !expiry.HasValue)
                {
                    client.SetValue(key, value);
                }
                else
                {
                    client.SetValue(key, value, expiry.Value);
                }
                return true;
            }
        }

        public static bool SetString<T>(string key, T obj, TimeSpan? expiry = new TimeSpan?())
        {
            string sortedSetValue = GetSortedSetValue(obj);
            return SetString(key, sortedSetValue, expiry);
        }

        public static long SortedSetLength(string key)
        {
            key = GetRedisKey(key);
            using (IRedisClient client = RedisClientManager.GetReadOnlyClient())
            {
                return client.GetSortedSetCount(key, double.MinValue, double.MaxValue);
            }
        }

        public static PooledRedisClientManager RedisClientManager =>  _redisHelper.RedisClientsManager;
    }

}
