/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * Coursework 1 - Scanner and Parser
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	// simple class to keep track of token's position
	public class Location
	{
		public int lineNumber;
		public int linePos;

		public Location(int lineNum, int linePos)
		{
			this.lineNumber = lineNum;
			this.linePos = linePos;
		}

		public override string ToString()
		{
			String output = "";
			output += "Line number: " + this.lineNumber + ", line index: " + this.linePos;
			return output;
		}

	}
}
