using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th14Generator : ThGenerator
    {
        private const int MaxCardNumber = 120;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RA", "RB", "MA", "MB", "SA", "SB" };

        private static readonly IEnumerable<string> CharasWithTotal =
            Charas.Concat(new string[] { "TL" });

        private static readonly IEnumerable<int> CardNumbers =
            InitializeCardNumbers(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithTotal =
            InitializeCardNumbersWithTotal(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithIrregal =
            InitializeCardNumbersWithIrregal(MaxCardNumber);

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th14.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var kinds = new string[] { "S", "P" };
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T14SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T14C", kinds, numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T14CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T14CRG", kinds, LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T14CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T14CHARA", CharasWithTotal, Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T14CHARAEX", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T14PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal)
                };

                foreach (var formats in formatsList)
                {
                    foreach (var format in formats)
                        writer.WriteLine(format);
                    writer.WriteLine();
                }
            }
        }
    }
}
