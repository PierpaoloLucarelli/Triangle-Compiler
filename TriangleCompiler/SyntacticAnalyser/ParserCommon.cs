/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System.Collections.Generic;
namespace TriangleCompiler.SyntacticAnalyser
{
	public partial class Parser
	{

		private Scanner _scanner;

		//private ErrorReporter ErrReporter;

		private Token _currentToken;

		IEnumerator<Token> _tokens;

		public Parser(Scanner scanner)
		{
			_scanner = scanner;
			_tokens = _scanner.GetEnumerator();
			//ErrReporter = new ErrorReporter();
		}

		// Checks that the kind of the current token matches the expected kind, and
		// fetches the next token from the source file
		public void Accept(TokenKind expectedKind)
		{
			if (_currentToken.Kind == expectedKind)
			{
				Token token = _currentToken;
				//_previousLocation = token.Start;
				_tokens.MoveNext();
				_currentToken = _tokens.Current;
			}
			else
			{
				string errMsg = "Expected: " + expectedKind + " but got: " + _currentToken.Kind;
				ErrorReporter.ReportError(errMsg, _currentToken);
			}
		}

		// Just Fetches the next token from the source file.
		void AcceptIt()
		{
			_tokens.MoveNext();
			_currentToken = _tokens.Current;
		}
	}
}
