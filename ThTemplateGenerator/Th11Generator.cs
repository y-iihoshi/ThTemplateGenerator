using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th11Generator : ThGenerator
    {
        private const int MaxCardNumber = 175;

        private static readonly IEnumerable<string> Charas =
            new string[] { "RY", "RS", "RA", "MA", "MP", "MN" };

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
            using (var writer = new StreamWriter(Path.Combine(directory, "th11.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T11SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T11C", numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T11CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T11CRG", LevelsWithTotal, CharasWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T11CLEAR", LevelsWithTotal, CharasWithTotal),
                    GenerateFormats(
                        "%T11CHARA", CharasWithTotal, Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T11CHARAEX", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T11PRAC", LevelsWithTotal, CharasWithTotal, StagesWithTotal)
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
