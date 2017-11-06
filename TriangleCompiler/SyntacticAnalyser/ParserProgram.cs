/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * Coursework 1 - Scanner and Parser
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{

		// main entry to parsing the program
		public void ParseProgram()
		{
			Console.WriteLine("parsing Program");
			_tokens.MoveNext();
			_currentToken = _tokens.Current;
			ParseCommand();
		}
	}
}
