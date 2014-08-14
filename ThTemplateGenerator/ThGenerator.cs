using System;
using System.Collections.Generic;
using System.Linq;

namespace ThTemplateGenerator
{
    class ThGenerator
    {
        protected static readonly Func<int, string> ToDefaultString = n => n.ToString();

        protected static readonly IEnumerable<string> Levels =
            new string[] { "E", "N", "H", "L", "X" };

        protected static readonly IEnumerable<string> LevelsWithTotal =
            Levels.Concat(new string[] { "T" });

        protected static readonly IEnumerable<string> Stages =
            Enumerable.Range(1, 6).Select(ToDefaultString);

        protected static readonly IEnumerable<string> StagesWithTotal =
            Enumerable.Repeat(0, 1).Select(ToDefaultString).Concat(Stages);

        protected static readonly IEnumerable<string> Ranks =
            Enumerable.Range(1, 9).Concat(Enumerable.Repeat(0, 1)).Select(ToDefaultString);

        protected static readonly IEnumerable<string> CardInfoTypes =
            new string[] { "N", "R" };

        protected ThGenerator() { }

        public virtual void Generate(string directory)
        {
            throw new NotImplementedException();
        }

        protected static IEnumerable<int> InitializeCardNumbers(int max)
        {
            return Enumerable.Range(1, max);
        }

        protected static IEnumerable<int> InitializeCardNumbersWithTotal(int max)
        {
#if false
            return Enumerable.Repeat(0, 1).Concat(InitializeCardNumbers(max));
#else
            return Enumerable.Range(0, max + 1);
#endif
        }

        protected static IEnumerable<int> InitializeCardNumbersWithIrregal(int max)
        {
#if false
            return InitializeCardNumbersWithTotal(max).Concat(Enumerable.Repeat(max + 1, 1));
#else
            return Enumerable.Range(0, max + 2);
#endif
        }

        protected static IEnumerable<string> GenerateFormats(
            string prefix, params IEnumerable<string>[] parameters)
        {
            return Utils.CrossProduct(parameters).Select(prod => prefix + string.Join("", prod.ToArray()));
        }
    }
}
