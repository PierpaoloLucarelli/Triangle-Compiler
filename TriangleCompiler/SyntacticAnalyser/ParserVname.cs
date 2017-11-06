/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{
		void ParseVname()
		{
			Console.WriteLine("parsing variable name");
			// vnames are formed by identifiers
			ParseIdentifier();
		}

	}
}