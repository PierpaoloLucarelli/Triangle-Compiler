using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{
		void ParseIdentifier()
		{
			System.Console.WriteLine("parsing identifier");
			Accept(TokenKind.Identifier);
		}

		void ParseIntLiteral()
		{
			System.Console.WriteLine("parsing int literal");
			Accept(TokenKind.IntLiteral);
		}



		void ParseTypeDenoter()
		{
			System.Console.WriteLine("parsing type denoter");
			ParseIdentifier();
		}

	}
}
