using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th06Generator : ThGenerator
    {
        private const int MaxCardNumber = 64;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RA", "RB", "MA", "MB" };

        private static readonly IEnumerable<string> CharasWithTotal =
            Charas.Concat(new string[] { "TL" });

        private static readonly new IEnumerable<string> Stages =
            ThGenerator.Stages.Concat(new string[] { "X" });

        private static readonly new IEnumerable<string> StagesWithTotal =
            Enumerable.Repeat(0, 1).Select(ToDefaultString).Concat(Stages);

        private static readonly IEnumerable<int> CardNumbers =
            InitializeCardNumbers(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithTotal =
            InitializeCardNumbersWithTotal(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithIrregal =
            InitializeCardNumbersWithIrregal(MaxCardNumber);

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th06.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D2"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T06SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T06C", numbersWithIrregal, Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T06CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T06CRG", StagesWithTotal, Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T06CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T06PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal)
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
