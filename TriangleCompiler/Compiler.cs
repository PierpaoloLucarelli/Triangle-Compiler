using System;

namespace TriangleCompiler
{
    SourceFile _source;

    class Compiler
    {

        Compiler(string sourceFileName)
        {
            _source = new SourceFile(sourceFileName);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Running Triangle Compiler");
            var sourceFileName = args[0];
            if (sourceFileName != null)
            {
                var compiler = new Compiler(sourceFileName);
            }
        }
    }
}
