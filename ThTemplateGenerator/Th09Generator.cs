using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th09Generator : ThGenerator
    {
        private static readonly IEnumerable<string> Charas =
            new string[]
            {
                "RM", "MR", "SK", "YM", "RS", "CI", "LY", "MY", "TW", "AY", "MD", "YU", "KM", "SI"
            };

        private static readonly IEnumerable<string> CharasWithTotal =
            Charas.Concat(new string[] { "TL" });

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th09.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T09SCR", LevelsWithTotal, CharasWithTotal, Ranks,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T09CLEAR", LevelsWithTotal, CharasWithTotal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T09TIMEALL")
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
