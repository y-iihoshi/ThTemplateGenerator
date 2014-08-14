using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th125Generator : ThGenerator
    {
        private static readonly new IEnumerable<string> Levels =
            Enumerable.Range(1, 9)
                .Select(ToDefaultString)
                .Concat(new string[] { "A", "B", "C", "X", "S" });

        private static readonly new IEnumerable<string> LevelsWithTotal =
            Levels.Concat(new string[] { "T" });

        private static readonly IEnumerable<string> Charas =
            new string[] { "A", "H" };

        private static readonly IEnumerable<string> CharasWithTotal =
            Charas.Concat(new string[] { "T" });

        private static readonly IEnumerable<string> Scenes =
            Enumerable.Range(1, 9).Select(ToDefaultString);

        private static readonly IEnumerable<string> ScenesWithIrregal =
            Enumerable.Repeat(0, 1).Select(ToDefaultString).Concat(Scenes);

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th125.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T125SCR", CharasWithTotal, LevelsWithTotal, ScenesWithIrregal,
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T125SCRTL", CharasWithTotal, Enumerable.Range(1, 2).Select(ToDefaultString),
                        Enumerable.Range(1, 5).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T125CARD", LevelsWithTotal, ScenesWithIrregal,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T125TIMEPLY"),
                    GenerateFormats(
                        "%T125SHOT", CharasWithTotal, LevelsWithTotal, ScenesWithIrregal),
                    GenerateFormats(
                        "%T125SHOTEX", CharasWithTotal, LevelsWithTotal, ScenesWithIrregal,
                        Enumerable.Range(1, 7).Select(ToDefaultString))
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
