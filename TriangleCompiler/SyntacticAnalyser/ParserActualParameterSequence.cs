/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;

namespace TriangleCompiler.SyntacticAnalyser
{
	partial class Parser
	{
		void ParseActualParameterSequence()
		{
			Console.WriteLine("parsing actual parameter sequence");
			// parameter sequence may be empty if we find a right parenthesis
			if (_currentToken.Kind != TokenKind.RightParen)
			{
				ParseActualParameter();
				while (_currentToken.Kind == TokenKind.Comma)
				{
					AcceptIt();
					ParseActualParameter();
				}
			}

		}

		void ParseActualParameter()
		{
			Console.WriteLine("parsing actual parameter");
			switch (_currentToken.Kind)
			{
				case (TokenKind.Var):
					{
						AcceptIt();
						ParseVname();
						break;
					}
				// check if we have an expression
				case TokenKind.Identifier:
				case TokenKind.IntLiteral:
				case TokenKind.CharLiteral:
				case TokenKind.Operator:
				case TokenKind.LeftParen:
					{
						ParseExpression();
						break;
					}

			}
		}
	}
}
