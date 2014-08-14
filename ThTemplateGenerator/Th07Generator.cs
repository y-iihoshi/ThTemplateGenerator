using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th07Generator : ThGenerator
    {
        private const int MaxCardNumber = 141;

        private static readonly new IEnumerable<string> Levels =
            ThGenerator.Levels.Concat(new string[] { "P" });

        private static readonly new IEnumerable<string> LevelsWithTotal =
            Levels.Concat(new string[] { "T" });

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
            using (var writer = new StreamWriter(Path.Combine(directory, "th07.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T07SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T07C", numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T07CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T07CRG", LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T07CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T07PLAY", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T07TIMEALL"),
                    GenerateFormats(
                        "%T07TIMEPLY"),
                    GenerateFormats(
                        "%T07PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString))
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
