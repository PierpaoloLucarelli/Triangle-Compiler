/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    public partial class Parser
    {
        public void ParseTypeDenoter()
        {
            Console.WriteLine("parsing type denoter");
			// type denoter is formed by an identifier
			ParseIdentifier();
        }
    }
}
