using System;
using System.Collections.Generic;
namespace TriangleCompiler.SyntacticAnalyser
{
    public partial class Parser
    {
        public void ParseTypeDenoter()
        {
            System.Console.WriteLine("parsing type denoter");
            ParseIdentifier();
        }
    }
}
