using System;
namespace TriangleCompiler.SyntacticAnalyser
{
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
