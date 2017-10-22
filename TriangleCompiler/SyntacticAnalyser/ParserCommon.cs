using System;
using System.Collections.Generic;
namespace TriangleCompiler.SyntacticAnalyser
{
    public partial class Parser
    {

        private Scanner _scanner;

        private Token _currentToken;

        IEnumerator<Token> _tokens;

        public Parser(Scanner scanner){
			_scanner = scanner;
            _tokens = _scanner.GetEnumerator();
        }

		// Checks that the kind of the current token matches the expected kind, and
		// fetches the next token from the source file, if not it throws a
		public void Accept(TokenKind expectedKind){
			if (_currentToken.Kind == expectedKind)
			{
				Token token = _currentToken;
				//_previousLocation = token.Start;
				_tokens.MoveNext();
				_currentToken = _tokens.Current;
			}
        }

		// Just Fetches the next token from the source file.
		void AcceptIt(){
            //_previousLocation = _currentToken.Finish;
            _tokens.MoveNext();
            _currentToken = _tokens.Current;
        }
    }
}
