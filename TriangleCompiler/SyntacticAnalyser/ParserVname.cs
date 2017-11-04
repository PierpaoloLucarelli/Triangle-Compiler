using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
    {
        void ParseVname()
        {
            System.Console.WriteLine("parsing variable name");
            ParseIdentifier();
        }

    }
}