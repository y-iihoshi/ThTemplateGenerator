using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th10Generator : ThGenerator
    {
        private const int MaxCardNumber = 110;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RA", "RB", "RC", "MA", "MB", "MC" };

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
            using (var writer = new StreamWriter(Path.Combine(directory, "th10.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T10SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T10C", numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T10CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T10CRG", LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T10CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T10CHARA", CharasWithTotal, Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T10CHARAEX", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T10PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal)
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
