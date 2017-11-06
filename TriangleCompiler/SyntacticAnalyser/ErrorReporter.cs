/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    public static class ErrorReporter
    {
        private static bool ErrFlag = false;
        private static int ErrCountParse = 0;
        private static int ErrCountScan = 0;

        // prints out an error for the parser
        public static void ReportError(String msg, Token tk) {
            ErrFlag = true;
            ErrCountParse++;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + msg + " from token: " + tk);
            Console.ResetColor();

        }

		// prints out an error for the scanner
		public static void ReportError(String msg) {
			ErrFlag = true;
			ErrCountScan++;
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("ERROR: " + msg);
			Console.ResetColor();
		}

        public static bool HasErrors(){
            return ErrFlag;
        }

        public static int ErrorCountParse(){
            return ErrCountParse;
        }

        public static int ErrorCountScan(){
			return ErrCountParse;
		}

        public static String getErrorReport(){
            Console.ForegroundColor = (ErrFlag ? ConsoleColor.Red : ConsoleColor.Green);
            String output = "";
            output += "Program processed with: " + ErrCountScan +
                " scanning errors and " + ErrCountParse + " parsing errors";
            return output;
        }
    }
}
