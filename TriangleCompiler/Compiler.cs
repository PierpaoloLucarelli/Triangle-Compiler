/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * Coursework 1 - Scanner and Parser
 */

using System;
using TriangleCompiler.SyntacticAnalyser;

namespace TriangleCompiler
{

	class Compiler
	{
		SourceFile _source;
		Scanner _scanner; // the scanner
		Parser _parser; // the parser

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
					// parse the program
                    compiler._parser.ParseProgram();

					Console.WriteLine("\n---------------\n");
                    // print error report
					Console.WriteLine(ErrorReporter.getErrorReport());
				}
			}
            // no file name provided
			else
			{
				Console.WriteLine("Please specify a file name");
			}
		}
	}
}
