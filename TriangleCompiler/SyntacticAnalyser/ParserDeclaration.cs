/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{
		public void ParseDeclaration()
		{
			System.Console.WriteLine("parsing declaration");
			ParseSingleDeclaration();
			while (_currentToken.Kind == TokenKind.Semicolon)
			{
				AcceptIt(); // accept semicolon
				ParseSingleDeclaration();
			}
		}

		void ParseSingleDeclaration()
		{
			System.Console.WriteLine("parsing single declaration");
			switch (_currentToken.Kind)
			{
				case TokenKind.Const:
					{
						AcceptIt(); // accept const token
						ParseIdentifier();
						Accept(TokenKind.Is);
						ParseExpression();
						break;
					}
				case TokenKind.Var:
					{
						AcceptIt(); // accept var token
						ParseIdentifier();
						if (_currentToken.Kind == TokenKind.Colon)
						{
							AcceptIt(); // accept colon token
							ParseTypeDenoter();
						}
						else
							// colon token is mandatory
							ErrorReporter.ReportError("Missing colon", _currentToken);
						break;

					}
				default:
					{
						ErrorReporter.ReportError("Cannot parse single declaration", _currentToken);
						break;
					}
			}
		}
	}
}
