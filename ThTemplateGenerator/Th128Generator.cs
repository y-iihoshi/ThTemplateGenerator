using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th128Generator : ThGenerator
    {
        private const int MaxCardNumber = 250;

        private static readonly IEnumerable<string> Routes =
            new string[] { "A1", "A2", "B1", "B2", "C1", "C2", "EX" };

        private static readonly IEnumerable<string> RoutesWithTotal =
            Routes.Concat(new string[] { "TL" });

        private static readonly new IEnumerable<string> Stages =
            new string[]
            {
                "A11", "A12", "A13", "A22", "A23", "B11", "B12", "B13", "B22", "B23",
                "C11", "C12", "C13", "C22", "C23"
            };

        private static readonly new IEnumerable<string> StagesWithTotal =
            Stages.Concat(new string[] { "TTL" });

        private static readonly IEnumerable<int> CardNumbers =
            InitializeCardNumbers(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithTotal =
            InitializeCardNumbersWithTotal(MaxCardNumber);

        private static readonly IEnumerable<int> CardNumbersWithIrregal =
            InitializeCardNumbersWithIrregal(MaxCardNumber);

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th128.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var numbersWithIrregal = CardNumbersWithIrregal.Select(n => n.ToString("D3"));
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T128SCR", LevelsWithTotal, RoutesWithTotal, Ranks,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T128C", numbersWithIrregal, Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T128CARD", numbersWithIrregal, CardInfoTypes),
                    GenerateFormats(
                        "%T128CRG", LevelsWithTotal, StagesWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T128CLEAR", LevelsWithTotal, RoutesWithTotal),
                    GenerateFormats(
                        "%T128ROUTE", RoutesWithTotal, Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T128ROUTEEX", LevelsWithTotal, RoutesWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T128TIMEPLY")
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
