using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using TriangleCompiler.SyntacticAnalyser;

namespace TriangleCompiler.SyntacticAnalyser
{
	public class Scanner : IEnumerable<Token>
	{
		SourceFile _source;

		StringBuilder _currentSpelling;

		bool _debug;

		public Scanner(SourceFile source)
		{
			_source = source;
			_source.Reset();
			_currentSpelling = new StringBuilder();
		}

		public Scanner EnableDebugging()
		{
			_debug = true;
			return this;
		}
		public IEnumerator<Token> GetEnumerator()
		{
			while (true)
			{
                int c = _source.Current;
				while (c == '!' || c == ' ' || c == '\t' || c == '\n')
				{
					ScanSeparator();
                    c = _source.Current;
				}

				_currentSpelling.Clear();

				//var startLocation = new Location(_source.Location.line_index, _source.Location.line_number);
				var kind = ScanToken();
				//var endLocation = new Location(_source.Location.line_index, _source.Location.line_number);
				//var position = new SourcePosition(startLocation, endLocation);
				//Console.WriteLine(position);

				var token = new Token(kind, _currentSpelling.ToString());
				if (_debug)
					Console.WriteLine(token);

				yield return token;
				if (token.Kind == TokenKind.EndOfText) { break; }
			}
		}

		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}


		// Appends the current character to the current token, and gets
		//the next character from the source program.

		void TakeIt()
		{
			_currentSpelling.Append((char)_source.Current);
			_source.MoveNext();
		}


		//Skip a single separator.

		void ScanSeparator()
		{
            if(_source.Current == '!'){
				_source.SkipRestOfLine();
				_source.MoveNext();
            } else{
                _source.MoveNext();
            }

		}
		TokenKind ScanToken()
        {
            if (_source.Current == -1)
                return TokenKind.EndOfText;

            // if current is a letter accept all next letters and digits in token
            if(IsLetter(_source.Current)){
                do TakeIt();
                while (IsLetter(_source.Current) || IsDigit(_source.Current));
                return TokenKind.Identifier;
            }

            // if its a digit also add next digita to current token
            if (IsDigit(_source.Current)){
                do TakeIt();
                while (IsDigit(_source.Current));
                return TokenKind.IntLiteral;
            }

            // if it's an operator take it
            if(IsOperator(_source.Current)){
                do TakeIt();
                while (IsOperator(_source.Current));
                return TokenKind.Operator;
            }

            else {
                TakeIt();
                return TokenKind.Error;
            }

		}

		bool IsLetter(int ch)
		{
			return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
		}

		bool IsDigit(int ch)
		{
			return '0' <= ch && ch <= '9';
		}

		bool IsOperator(int ch)
		{
			switch (ch)
			{
				case '+':
				case '-':
				case '*':
				case '/':
				case '=':
				case '<':
				case '>':
				case '\\':
				case '&':
				case '@':
				case '%':
				case '^':
				case '?':
					return true;

				default:
					return false;
			}
		}
	}
}