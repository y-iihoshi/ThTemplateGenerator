using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th123Generator : ThGenerator
    {
        private const int MaxCardNumber = 64;

        private static readonly IEnumerable<string> Charas =
            new string[]
            {
                "RM", "MR", "SK", "AL", "PC", "YM", "RL", "YU", "YK", "SU", "RS", "AY", "KM", "IK", "TN",
                "SN", "CI", "ML", "UT", "SW"
            };

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
            using (var writer = new StreamWriter(Path.Combine(directory, "th123.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D2"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T123C", numbersWithIrregal, CharasWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T123CARD", numbersWithIrregal, CharasWithTotal, CardInfoTypes),
                    GenerateFormats(
                        "%T123CRG", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T123DC", CharasWithTotal, new string[] { "Y", "K", "P" },
                        Enumerable.Range(1, 21).Select(n => n.ToString("D2")), new string[] { "N", "C" })
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
