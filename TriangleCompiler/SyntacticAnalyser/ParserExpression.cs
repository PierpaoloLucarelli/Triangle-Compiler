using System;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{
		void ParseExpression()
		{
			System.Console.WriteLine("Parsing expression");
			//ParseSecondaryExpression();
			//while (_currentToken.Kind == TokenKind.Operator)
			//{
			//	AcceptIt();
			//	ParsePrimaryExpression();
			//}
            switch(_currentToken.Kind){
                case TokenKind.Let:{
                        AcceptIt();
                        ParseDeclaration();
                        Accept(TokenKind.In);
                        ParseExpression();
                        break;   
                    }
                case TokenKind.If:{
                        AcceptIt();
                        ParseExpression();
                        Accept(TokenKind.Then);
                        ParseExpression();
                        Accept(TokenKind.Else);
                        ParseExpression();
                        break;
                    }
                default:
                    {
                        ParseSecondaryExpression();
                        break;
                    }                    
            }
		}


		void ParseSecondaryExpression()
		{
			System.Console.WriteLine("parsing secondary expression");

		}

		void ParsePrimaryExpression()
		{
			System.Console.WriteLine("parsing primary expression");
			switch (_currentToken.Kind)
			{
				case TokenKind.IntLiteral:
					{
						ParseIntLiteral();
						break;
					}
				case TokenKind.Identifier:
					{
						ParseIdentifier();
						break;
					}
				case TokenKind.Operator:
					{
						AcceptIt();
						ParsePrimaryExpression();
						break;

					}
				case TokenKind.LeftParen:
					{
						AcceptIt();
						ParseExpression();
						break;
					}
				case TokenKind.RightParen:
					{
						AcceptIt();
						break;
					}
				default:
					System.Console.WriteLine("error");
					break;
			}
		}


	}
}
