namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{
		// Parses the command error
		void ParseCommand()
		{
			System.Console.WriteLine("parsing command");
			ParseSingleCommand();
			while (_currentToken.Kind == TokenKind.Semicolon)
			{
				AcceptIt();
				ParseSingleCommand();
			}
		}


		/// Parses the single command

		void ParseSingleCommand()
		{
			System.Console.WriteLine("parsing single command");
			switch (_currentToken.Kind)
			{

				case TokenKind.Identifier:
					{
						ParseIdentifier();
						//Accept(TokenKind.Becomes);
						//ParseExpression();
						break;

					}

				case TokenKind.Begin:
					{
						AcceptIt();
						ParseCommand();
						break;
					}
				case TokenKind.End:
					{
						AcceptIt();
						break;
					}
				case TokenKind.If:
					{
						AcceptIt();
						ParseExpression();
						break;
					}
				case TokenKind.Then:
					{
						AcceptIt();
						ParseSingleCommand();
						break;
					}
				case TokenKind.Else:
					{
						AcceptIt();
						ParseSingleCommand();
						break;
					}
				case TokenKind.While:
					{
						AcceptIt();
						ParseExpression();
						break;
					}
				case TokenKind.Do:
					{
						AcceptIt();
						ParseSingleCommand();
						break;
					}
				case TokenKind.Let:
					{
						AcceptIt();
						ParseDeclaration();
						break;
					}
				case TokenKind.In:
					{
						AcceptIt();
						ParseSingleCommand();
						break;
					}

				default:
					System.Console.WriteLine("error");
					break;

			}
		}
	}
}