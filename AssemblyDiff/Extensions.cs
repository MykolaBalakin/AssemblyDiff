using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balakin.AssemblyDiff {
    internal static class Extensions {
        public static Boolean NullOrEmpty<T>(this ICollection<T> collection) {
            return collection == null || collection.Count == 0;
        }

        public static T GetOrNull<T>(this IList<T> list, Int32 index)
            where T : class {
            if (list.NullOrEmpty()) {
                return null;
            }
            if (index >= list.Count) {
                return null;
            }
            return list[index];
        }

        public static T? GetOrNullableNull<T>(this IList<T> list, Int32 index)
           where T : struct {
            if (list.NullOrEmpty()) {
                return null;
            }
            if (index >= list.Count) {
                return null;
            }
            return list[index];
        }

        public static IEnumerable<T> UnionIfNotNull<T>(this IEnumerable<T> first, IEnumerable<T> second) {
            if (first == null) {
                return second;
            }
            if (second == null) {
                return first;
            }
            return first.Union(second);
        }

        public static TValue GetOrNull<TKey,TValue>(this IDictionary<TKey,TValue> dict, TKey key)
           where TValue : class {
            if (dict == null) {
                return null;
            }
            if (!dict.ContainsKey(key)) {
                return null;
            }
            return dict[key];
        }

        public static TValue? GetOrNullableNull<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey key)
        where TValue : struct {
            if (dict == null) {
                return null;
            }
            if (!dict.ContainsKey(key)) {
                return null;
            }
            return dict[key];
        }
    }
}
