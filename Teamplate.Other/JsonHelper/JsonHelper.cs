using Newtonsoft.Json;
using System.Collections.Generic;

namespace Teamplate.NuGet
{
    public static class JsonHelper
    {
        // Methods
        public static T Deserialize<T>(this string json)
        {
            if (json != null)
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            return default(T);
        }

        public static List<T> DeserializeToList<T>(this string json)
        {
            if (json != null)
            {
                return JsonConvert.DeserializeObject<List<T>>(json);
            }
            return null;
        }

        public static string Serialize(this object obj) =>
            JsonConvert.SerializeObject(obj);
    }
}
