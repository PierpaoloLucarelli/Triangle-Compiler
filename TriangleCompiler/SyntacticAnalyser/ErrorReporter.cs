/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    public class ErrorReporter
    {
        private bool ErrFlag;
        private int ErrCount;

        public ErrorReporter()
        {
            this.ErrFlag = false;
            this.ErrCount = 0;
        }

        // prints out an error
        public void ReportError(String msg, Token tk) {
            ErrFlag = true;
            ErrCount++;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("ERROR: " + msg + " from token: " + tk);
            Console.ResetColor();

        }

        public bool HasErrors(){
            return this.ErrFlag;
        }

        public int ErrorCount(){
            return this.ErrCount;
        }
    }
}
