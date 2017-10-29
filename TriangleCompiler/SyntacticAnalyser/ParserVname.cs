using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
    {
        void ParseVname()
        {
            System.Console.WriteLine("Parsing variable name");
            ParseIdentifier();
        }

    }
}