/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    // class to contain a start pos and an end pos
    public class SourcePosition
    {
        public Location startPosition;
        public Location endPosition;
        public SourcePosition(Location s, Location e)
        {
            this.startPosition = s;
            this.endPosition = e;
        }

        public override string ToString() {
            String output = "";
            output += "Start position: " + this.startPosition + ", End position: " + this.endPosition;
            return output;
        }
	}
}
