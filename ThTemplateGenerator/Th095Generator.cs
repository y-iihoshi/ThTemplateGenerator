using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th095Generator : ThGenerator
    {
        private static readonly new IEnumerable<string> Levels =
            Enumerable.Range(1, 9)
                .Concat(Enumerable.Repeat(0, 1))
                .Select(ToDefaultString)
                .Concat(new string[] { "X" });

        private static readonly new IEnumerable<string> LevelsWithTotal =
            Levels.Concat(new string[] { "T" });

        private static readonly IEnumerable<string> Scenes =
            Enumerable.Range(1, 9).Select(ToDefaultString);

        private static readonly IEnumerable<string> ScenesWithIrregal =
            Enumerable.Repeat(0, 1).Select(ToDefaultString).Concat(Scenes);

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th095.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T95SCR", LevelsWithTotal, ScenesWithIrregal,
                        Enumerable.Range(1, 4).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T95SCRTL", Enumerable.Range(1, 4).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T95CARD", LevelsWithTotal, ScenesWithIrregal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T95SHOT", LevelsWithTotal, ScenesWithIrregal),
                    GenerateFormats(
                        "%T95SHOTEX", LevelsWithTotal, ScenesWithIrregal,
                        Enumerable.Range(1, 6).Select(ToDefaultString))
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
