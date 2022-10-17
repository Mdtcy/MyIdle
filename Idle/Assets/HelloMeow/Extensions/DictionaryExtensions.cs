using System.Collections.Generic;
using System.Text;

namespace HM.Extensions
{
    public static class DictionaryExtensions
    {
        // Works in C#3/VS2008:
        // Returns a new dictionary of this ... others merged leftward.
        // Keeps the type of 'this', which must be default-instantiable.
        // Example:
        //   result = map.MergeLeft(other1, other2, ...)
        public static Dictionary<TKey, TValue> Merge<TKey, TValue>(IEnumerable<Dictionary<TKey, TValue>> dictionaries)
        {
            var result = new Dictionary<TKey, TValue>();
            foreach (var dict in dictionaries)
                foreach (var x in dict)
                    result[x.Key] = x.Value;
            return result;
        }

        public static TValue GetValue<TKey, TValue>(this Dictionary<TKey, TValue> self, TKey key, TValue defaultValue)
        {
            return self.ContainsKey(key) ? self[key] : defaultValue;
        }

        public static void SafeAdd<TKey, TValue>(this Dictionary<TKey, TValue> self,
                                                 TKey                          key,
                                                 TValue                        value)
        {
            if (!self.ContainsKey(key))
            {
                self.Add(key, value);
            }
        }

        public static void ForceAdd<TKey, TValue>(this Dictionary<TKey, TValue> self,
                                                 TKey                          key,
                                                 TValue                        value)
        {
            if (self.ContainsKey(key))
            {
                self.Remove(key);
            }
            self.Add(key, value);
        }

        public static TValue GetOrAddValue<TKey, TValue>(this Dictionary<TKey, TValue> self,
                                                         TKey key,
                                                         TValue defaultValue)
        {
            if (!self.ContainsKey(key))
            {
                self[key] = defaultValue;
            }
            return self[key];
        }

        public static string DictToString<TKey, TValue>(this Dictionary<TKey, TValue> dict)
        {
            if (dict == null || dict.Count == 0) return "{}";
            var sb = new StringBuilder();

            foreach (var key in dict.Keys)
            {
                var value = dict[key];
                sb.Append($"({key}:{value}),");
            }

            return sb.ToString();
        }
    }
}