/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using System;
using System.IO;

namespace Triangle.Compiler
{
    class StreamErrorReporter : ErrorReporter
    {
        private static StreamErrorReporter instance;

        TextWriter _output;
        
        int _errorCount;

        private StreamErrorReporter()
        {
            _output = Console.Out;
            _errorCount = 0;
        }

        public static StreamErrorReporter Instance
		{
			get
			{
				if (instance == null)
				{
                    instance = new StreamErrorReporter();
				}
				return instance;
			}
		}

        public void ReportError(string message, string tokenName, SourcePosition pos)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            _output.WriteLine("ERROR: {0} {1}", message.Replace("%", tokenName), pos);
            _errorCount++;
            Console.ResetColor();
        }

        public void ReportRestriction(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            _output.WriteLine("RESTRICTION: {0}", message);
            Console.ResetColor();
        }

        public void ReportMessage(string message)
        {
           _output.WriteLine(message);
        }

        public int ErrorCount { get { return _errorCount; } }

        public bool HasErrors { get { return _errorCount > 0; } }
    }
}