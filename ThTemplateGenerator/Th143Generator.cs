using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th143Generator : ThGenerator
    {
        private static readonly IEnumerable<string> Days =
            Enumerable.Range(1, 9)
                .Select(ToDefaultString)
                .Concat(new string[] { "L" });

        private static readonly IEnumerable<string> DaysWithTotal =
            Days.Concat(new string[] { "T" });

        private static readonly IEnumerable<string> Scenes =
            Enumerable.Range(1, 9).Concat(Enumerable.Repeat(0, 1)).Select(ToDefaultString);

        private static readonly IEnumerable<string> Items =
            Enumerable.Range(1, 9).Concat(Enumerable.Repeat(0, 1)).Select(ToDefaultString);

        private static readonly IEnumerable<string> ItemsWithTotal =
            Items.Concat(new string[] { "T" });

        private static readonly IEnumerable<string> Nicknames =
            Enumerable.Range(1, 70).Select(n => n.ToString("D2"));

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th143.txt")))
            {
                // Using -WithTotal and -WithIrregal variables is for boundary value analysis.
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats(
                        "%T143SCR", DaysWithTotal, Scenes, ItemsWithTotal,
                        Enumerable.Range(1, 3).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T143SCRTL", ItemsWithTotal, Enumerable.Range(1, 4).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T143CARD", DaysWithTotal, Scenes,
                        Enumerable.Range(1, 2).Select(ToDefaultString)),
                    GenerateFormats(
                        "%T143NICK", Nicknames),
                    GenerateFormats(
                        "%T143TIMEPLY"),
                    GenerateFormats(
                        "%T143SHOT", DaysWithTotal, Scenes),
                    GenerateFormats(
                        "%T143SHOTEX", DaysWithTotal, Scenes,
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
