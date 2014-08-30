using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ThTemplateGenerator
{
    class Th135Generator : ThGenerator
    {
        private const int MaxCardNumber = 64;

        private static readonly IEnumerable<string> Charas =
            new string[]
            {
                "RM", "MR", "BY", "MK", "IU", "FT", "NT", "KO", "MM", "KK"
            };

        private static readonly IEnumerable<string> CharasWithTotal =
            Charas.Concat(new string[] { "TL" });

        public override void Generate(string directory)
        {
            using (var writer = new StreamWriter(Path.Combine(directory, "th135.txt")))
            {
                var formatsList = new IEnumerable<string>[]
                {
                    GenerateFormats("%T135CLEAR"),
                    GenerateFormats("%T135CHARA", CharasWithTotal)
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
