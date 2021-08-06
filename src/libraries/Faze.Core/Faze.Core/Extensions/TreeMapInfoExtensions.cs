using Faze.Core.TreeLinq;
using System.Collections.Generic;
using System.Linq;

namespace Faze.Core.Extensions
{
    public static class TreeMapInfoExtensions
    {
        public static string GetPathHash(this TreeMapInfo info, int? maxDepth = null, bool ordered = false)
        {
            IEnumerable<int> path = info.GetPath();

            if (maxDepth.HasValue)
                path = path.Take(maxDepth.Value);

            if (ordered)
                path = path.OrderBy(x => x);

            return string.Join(",", path);
        }
    }
}
