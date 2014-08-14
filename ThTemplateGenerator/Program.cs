namespace ThTemplateGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            var outputDirectory = (args.Length > 0) ? args[0] : ".";
            var generators = new ThGenerator[]
            {
                new Th06Generator(),
                new Th07Generator(),
                new Th075Generator(),
                new Th08Generator(),
                new Th09Generator(),
                new Th095Generator(),
                new Th10Generator(),
                new Th105Generator(),
                new Th11Generator(),
                new Th12Generator(),
                new Th125Generator(),
                new Th128Generator(),
                new Th13Generator(),
                new Th14Generator(),
                new Th143Generator()
            };

            foreach (var generator in generators)
                generator.Generate(outputDirectory);
        }
    }
}
