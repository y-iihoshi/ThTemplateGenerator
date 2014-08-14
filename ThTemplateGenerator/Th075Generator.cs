using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th075Generator : ThGenerator
    {
        private const int MaxCardNumber = 100;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RM", "MR", "SK", "AL", "PC", "YM", "RL", "YU", "YK", "SU" };

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
            using (var writer = new StreamWriter(Path.Combine(directory, "th075.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T75SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T75C", numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 4).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T75CARD", numbersWithIrregal, CharasWithTotal, CardInfoTypes),
                    GenerateFormats(
                        "%T75CRG", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T75CHR", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 4).Select(ToDefaultString))
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
