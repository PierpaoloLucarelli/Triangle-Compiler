using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    public partial class Parser
    {


		public void ParseProgram()
		{
			System.Console.WriteLine("parsing Program");
			_tokens.MoveNext();
			_currentToken = _tokens.Current;
			//var startLocation = _currentToken.Start;
			ParseCommand();
		}
    }
}
