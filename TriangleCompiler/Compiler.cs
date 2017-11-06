/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
using TriangleCompiler.SyntacticAnalyser;

namespace TriangleCompiler
{

	class Compiler
	{
		SourceFile _source;
		Scanner _scanner;
		Parser _parser;

		Compiler(string sourceFileName)
		{
			this._source = new SourceFile(sourceFileName);
			_scanner = new Scanner(_source);
			_parser = new Parser(_scanner);
		}

		static void Main(string[] args)
		{
			Console.WriteLine("Running Triangle Compiler");
            // read file name form cmd-line args
            if (args.Length > 0)
            {
                var sourceFileName = args[0];
                if (sourceFileName != null)
                {
                    var compiler = new Compiler(sourceFileName);
                    foreach (var token in compiler._scanner)
                    {
                        Console.WriteLine(token);
                    }
                    compiler._source.Reset();
                    Console.WriteLine("\n");
                    compiler._parser.ParseProgram();

                    Console.WriteLine("\n---------------\n");
                    int errCount = compiler._parser.GetErrCount();
                    Console.ForegroundColor = 
                        ( errCount > 0 ? ConsoleColor.Red : ConsoleColor.Green);
                    Console.WriteLine("Program parsed with " + errCount + 
                                      (errCount > 1 || errCount == 0 ? " errors" : " error"));
                    Console.ResetColor();
                }
			} else {
				Console.WriteLine("Please specify a file name");
			}
		}
	}
}
