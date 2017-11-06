/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{
		// Parses the command
		void ParseCommand()
		{
			System.Console.WriteLine("parsing command");
			ParseSingleCommand();
            // command may be followed by any number of commands separated by a ';'
			while (_currentToken.Kind == TokenKind.Semicolon)
			{
				AcceptIt(); // take the semicolon
				ParseSingleCommand();
			}
		}


		// Parses a single command
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
							AcceptIt(); // take become token
							ParseExpression();
						}
						else if (_currentToken.Kind == TokenKind.LeftParen)
						{
							AcceptIt(); // take left parent
                            ParseActualParameterSequence();
							Accept(TokenKind.RightParen);
						}
						break;

					}
				case TokenKind.If:
					{
						AcceptIt(); // take if token
						ParseExpression();
						Accept(TokenKind.Then);
						ParseSingleCommand();
						Accept(TokenKind.Else);
						ParseSingleCommand();
						break;
					}
				case TokenKind.While:
					{
						AcceptIt(); // take while token
						ParseExpression();
						Accept(TokenKind.Do);
						ParseSingleCommand();
						break;
					}
				case TokenKind.Let:
					{
                        AcceptIt(); // take let token
						ParseDeclaration();
						Accept(TokenKind.In);
						ParseSingleCommand();
						break;
					}
				case TokenKind.Begin:
					{
						AcceptIt(); // take begin token
                        ParseCommand();
						Accept(TokenKind.End);
						break;
					}
                // fix the trailing else problem
                case TokenKind.Semicolon:
                case TokenKind.End:
                case TokenKind.Else:
                case TokenKind.EndOfText:
                    {
                        break;
                    }
                default:
                    ErrReporter.ReportError("Cannot parse single command", _currentToken);
                    break;
			}
		}
	}
}