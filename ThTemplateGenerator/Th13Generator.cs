using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th13Generator : ThGenerator
    {
        private const int MaxCardNumber = 127;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RM", "MR", "SN", "YM" };

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
            using (var writer = new StreamWriter(Path.Combine(directory, "th13.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var kinds = new string[] { "S", "P" };
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T13SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T13C", kinds, numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T13CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T13CRG", kinds, LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T13CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T13CHARA", CharasWithTotal, Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T13CHARAEX", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T13PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal)
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
