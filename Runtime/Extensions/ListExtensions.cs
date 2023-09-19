using System.Collections.Generic;
using System.Linq;

namespace Packages.Estenis.UnityExts_
{
    public static class ListExtensions
    {
        public static T Pop<T>(this List<T> list)
        {
            var result = list.Last();
            list.RemoveAt(list.Count() - 1);
            return result;
        }
    }
}
