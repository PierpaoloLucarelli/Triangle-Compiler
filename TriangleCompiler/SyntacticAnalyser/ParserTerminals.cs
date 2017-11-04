/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
namespace TriangleCompiler.SyntacticAnalyser
{
    // terminals can be just accepted withouth extra checks
	public partial class Parser
	{
		void ParseIdentifier()
		{
			Console.WriteLine("parsing identifier");
			Accept(TokenKind.Identifier);
		}

        void ParseIntLiteral()
        {
            Console.WriteLine("parsing integer");
            Accept(TokenKind.IntLiteral);
        }

        void ParseOperator()
        {
            Console.WriteLine("parsing operator");
            Accept(TokenKind.Operator);
        }

        void ParseCharLiteral() {
            Console.WriteLine("parsing char literal");
            Accept(TokenKind.CharLiteral);
        }

	}
}
