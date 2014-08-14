using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th08Generator : ThGenerator
    {
        private const int MaxCardNumber = 222;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RY", "MA", "SR", "YY", "RM", "YK", "MR", "AL", "SK", "RL", "YM", "YU" };

        private static readonly IEnumerable<string> CharasWithTotal =
            Charas.Concat(new string[] { "TL" });

        private static readonly new IEnumerable<string> Stages =
            new string[] { "1A", "2A", "3A", "4A", "4B", "5A", "6A", "6B" };

        private static readonly new IEnumerable<string> StagesWithTotal =
            new string[] { "00" }.Concat(Stages);

        private static readonly IEnumerable<int> CardNumbers =
            InitializeCardNumbers(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithTotal =
            InitializeCardNumbersWithTotal(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithIrregal =
            InitializeCardNumbersWithIrregal(MaxCardNumber);

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th08.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var kinds = new string[] { "S", "P" };
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T08SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 9)
                            .Concat(Enumerable.Range(0, 1))
                            .Select(ToDefaultString)
                            .Concat(new string[] { "A", "B", "C", "D", "E", "F", "G" })),
                    GenerateFormats(
                        "%T08C", kinds, numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T08CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T08CRG", kinds, LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T08CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T08PLAY", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T08TIMEALL"),
                    GenerateFormats(
                        "%T08TIMEPLY"),
                    GenerateFormats(
                        "%T08PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal,
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
