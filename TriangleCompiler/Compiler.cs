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
			var sourceFileName = args[0];

			if (sourceFileName != null)
			{
				var compiler = new Compiler(sourceFileName);
				foreach (var token in compiler._scanner)
				{
					Console.WriteLine(token);
				}
				compiler._source.Reset(); //uncomment to reset source code.
				Console.WriteLine("\n");
				compiler._parser.ParseProgram();
			}
		}
	}
}
