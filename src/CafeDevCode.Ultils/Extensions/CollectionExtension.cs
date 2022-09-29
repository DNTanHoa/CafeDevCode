using CafeDevCode.Ultils.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CafeDevCode.Ultils.Extensions
{
    public static class CollectionExtension
    {
        public static IEnumerable<TreeList<T>> GenerateTree<T, K>(this IEnumerable<T> collection,
            Func<T, K> id,
            Func<T, K> parent, K root = default(K)!)
        {
            foreach (var item in collection.Where(c => parent(c)!.Equals(root)))
            {
                yield return new TreeList<T>
                {
                    Node = item,
                    Childrens = collection.GenerateTree(id, parent, id(item))
                };
            }
        }

        public static string GetRawData(this SortedList<string, string> requestData)
        {
            StringBuilder data = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in requestData)
            {
                if (!String.IsNullOrEmpty(kv.Value))
                {
                    data.Append(kv.Key + "=" + kv.Value + "&");
                }
            }
            if (data.Length > 0)
            {
                data.Remove(data.Length - 1, 1);
            }
            return data.ToString();
        }

        public static void AddData(this SortedList<string, string> data, string key, string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                data.Add(key, value);
            }
        }
    }
}
