using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    public class Location
    {
        public int lineNumber;
        public int linePos;

        public Location(int lineNum, int linePos)
        {
            this.lineNumber = lineNum;
            this.linePos = linePos;
        }

        public override string ToString() {
            String output = "";
            output += "Line number: " + this.lineNumber + ", line index: " + this.linePos;
            return output;
        }

	}
}
