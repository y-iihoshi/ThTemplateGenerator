using System.Collections.Generic;
using System.Linq;

namespace ThTemplateGenerator
{
    static class Utils
    {
        public static IEnumerable<IEnumerable<string>> CrossProduct(params IEnumerable<string>[] lists)
        {
            return CrossProduct(lists as IEnumerable<IEnumerable<string>>);
        }

        public static IEnumerable<IEnumerable<string>> CrossProduct(IEnumerable<IEnumerable<string>> lists)
        {
            return lists.Aggregate(
                Enumerable.Repeat(Enumerable.Empty<string>(), 1),
                (prod, list) => prod.SelectMany(
                    elem1 => list.Select(elem2 => elem1.Concat(Enumerable.Repeat(elem2, 1)))));
        }
    }
}
