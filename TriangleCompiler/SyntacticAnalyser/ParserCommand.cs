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


		// Parses the single command

		void ParseSingleCommand()
		{
            System.Console.WriteLine("parsing single command");
			switch (_currentToken.Kind)
			{
                
				case TokenKind.Identifier:
					{
                        ParseVname();
						if (_currentToken.Kind == TokenKind.Becomes)
						{
							AcceptIt();
							ParseExpression();
						}
						else if (_currentToken.Kind == TokenKind.LeftParen)
						{
							AcceptIt();
                            ParseActualParameterSequence();
							Accept(TokenKind.RightParen);
						}
						break;

					}
				case TokenKind.If:
					{
						AcceptIt();
						ParseExpression();
						Accept(TokenKind.Then);
						ParseSingleCommand();
						Accept(TokenKind.Else);
						ParseSingleCommand();
						break;
					}
				case TokenKind.While:
					{
						AcceptIt();
						ParseExpression();
						Accept(TokenKind.Do);
						ParseSingleCommand();
						break;
					}
				case TokenKind.Let:
					{
                        AcceptIt();
						ParseDeclaration();
						Accept(TokenKind.In);
						ParseSingleCommand();
						break;
					}
				case TokenKind.Begin:
					{
						AcceptIt();
                        ParseCommand();
						Accept(TokenKind.End);
						break;
					}
                case TokenKind.Semicolon:
                case TokenKind.End:
                case TokenKind.Else:
                case TokenKind.EndOfText:
                    {
                        break;
                    }
                default:
					System.Console.WriteLine("ERROR: Error in parsing command");
                    break;
			}
		}
	}
}