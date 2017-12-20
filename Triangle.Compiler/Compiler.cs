/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using System;
using Triangle.Compiler.CodeGenerator;
using Triangle.Compiler.ContextualAnalyzer;
using Triangle.Compiler.SyntacticAnalyzer;

namespace Triangle.Compiler
{

    public class Compiler
    {

        public static bool debug = true;
        /// <summary>
        /// The file for the object program, normally obj.tam.
        /// </summary>
        const string ObjectFileName = "obj.tam";

        /// <summary>
        /// The error reporter.
        /// </summary>
        ///
        // changed error reportr to use the singleton design pattern
        // this makes sure tht only one instance of the err reporter is shared between 
        // the compiler parts
        ErrorReporter ErrorReporter = StreamErrorReporter.Instance;

        /// <summary>
        /// The source file to compile.
        /// </summary>
        SourceFile _source;

        /// <summary>
        /// The lexical analyzer.
        /// </summary>
        Scanner _scanner;

        /// <summary>
        /// The syntactic analyzer.
        /// </summary>
        Parser _parser;
		
        /// <summary>
		/// The Checker.
		/// </summary>
		Checker _checker;

        Encoder _encoder;


        /// <summary>
        /// Creates a compiler for the given source file.
        /// </summary>
        /// <param name="sourceFileName">
        /// a File that specifies the source program
        /// </param>
        ///
        Compiler(string sourceFileName)
        {
            _source = new SourceFile(sourceFileName);
            _scanner = new Scanner(_source);//.EnableDebugging();
            _parser = new Parser(_scanner); // err reporter is passes statically
            _checker = new Checker(); // err reporter is passes statically
			_encoder = new Encoder(); // err reporter is passes statically
		}

        /// <summary>
        /// Compiles the source program to TAM machine code.
        /// </summary>
        /// <param name="showingTable">
        /// a boolean that determines if the object description details are to
        /// be displayed during code generation (not currently implemented)
        /// </param>
        /// <returns>
        /// true if the source program is free of compile-time errors, otherwise false
        /// </returns>
        ///
        bool CompileProgram()
        {

            ErrorReporter.ReportMessage("********** Triangle Compiler (C# Version 3.0) **********");

            if (!_source.IsValid)
            {
                ErrorReporter.ReportMessage("Cannot access source file \"" + _source.Name + "\".");
                return false;
            }

            // 1st pass
            ErrorReporter.ReportMessage("Syntactic Analysis ...");
            var program = _parser.ParseProgram();
            if (ErrorReporter.HasErrors)
            {
                ErrorReporter.ReportMessage("Compilation was unsuccessful: Syntactic analysis failed");
                return false;
            }

            // 2nd pass
            ErrorReporter.ReportMessage("Contextual analysis");
            _checker.Check(program);
            if (ErrorReporter.HasErrors)
            {
                ErrorReporter.ReportMessage("Compilation was unsuccessful: Contextual analysis failed");
                return false;
            }

			// 3rd pass
			ErrorReporter.ReportMessage("Code Generation ...");
			_encoder.EncodeRun(program);
			if (ErrorReporter.HasErrors)
			{
                ErrorReporter.ReportMessage("Compilation was unsuccessful: Code Generation failed.");
				return false;
			}

			// finally save the object code
			_encoder.SaveObjectProgram(ObjectFileName);

            Console.ForegroundColor = ConsoleColor.Green;
			ErrorReporter.ReportMessage("Compilation was successful.");
            Console.ResetColor();
            return true;
        }

        /// <summary>
        /// Triangle compiler main program.
        /// </summary>
        /// <param name="args">
        /// a string array containing the command-line arguments. This must
        /// be a single string specifying the source filename.
        /// </param>
        ///
        public static void Main(string[] args)
        {

            if (args.Length != 1)
            {
                //ErrorReporter.ReportMessage("Usage: Compiler.exe source");
                return;
            }

            var sourceFileName = args[0];

            if (sourceFileName != null)
            {
                var compiler = new Compiler(sourceFileName);
                compiler.CompileProgram();
            }
        }
    }
}