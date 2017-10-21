using System;
using System.Collections.Generic;
using System.Text;

namespace TriangleCompiler.SyntacticAnalyser
{
	public class SourcePosition
	{

		//Location start;
		//Location end;

		public SourcePosition(Location start, Location end)
		{
			this.start = start;
			this.end = end;
		}

		public override string ToString()
		{
			String output;
			output = "Start location: " + this.start + "\nEnd location: " + this.end;
			return output;
		}

	}
}
