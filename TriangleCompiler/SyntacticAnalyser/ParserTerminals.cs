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

        void ParseOperator()
        {
            System.Console.WriteLine("parsing operator");
            Accept(TokenKind.Operator);
        }

        void ParseCharLiteral() {
            System.Console.WriteLine("parsing char literal");
            Accept(TokenKind.CharLiteral);
        }

	}
}
