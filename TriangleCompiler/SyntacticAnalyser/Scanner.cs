/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace TriangleCompiler.SyntacticAnalyser {
    public class Scanner : IEnumerable<Token> {
        // contains source file
        SourceFile _source;
        // sontains spelling of current token
        StringBuilder _currentSpelling;

        bool _debug;


        public Scanner(SourceFile source) {
            _source = source;
            _source.Reset();
            _currentSpelling = new StringBuilder();
        }

        public Scanner EnableDebugging() {
            _debug = true;
            return this;
        }

        public IEnumerator<Token> GetEnumerator() {
            while (true) {
                int c = _source.Current;
                // ignore separators
                while (c == '!' || c == ' ' || c == '\t' || c == '\n') {
                    ScanSeparator();
                    c = _source.Current;
                }
                // clear current spelling for new word
                _currentSpelling.Clear();
                var startLocation = _source.getLocation();
                var kind = ScanToken();
                var endLocation = _source.getLocation();
                var position = new SourcePosition(startLocation, endLocation);

                //create new token based on spelling, kind and position 
                var token = new Token(kind, _currentSpelling.ToString(), position);
                if (_debug)
                    Console.WriteLine(token);

                yield return token;
                if (token.Kind == TokenKind.EndOfText) 
                    break;
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }


        // Appends the current character to the current token, and gets
        // the next character from the source program.
        void TakeIt() {
            _currentSpelling.Append((char)_source.Current);
            _source.MoveNext();
        }


        //Skip a line if comment or ignore if empty space
        void ScanSeparator() {
            if (_source.Current == '!') {
                _source.SkipRestOfLine();
                //_source.MoveNext();
            }
            else
                _source.MoveNext();
        }

        // group sequence of characters into recognized tokens
        // return the token kind
        TokenKind ScanToken()
        {

            if (_source.Current == -1)
                return TokenKind.EndOfText;

            // if current is a letter accept all next letters and digits in token
            if (IsLetter(_source.Current)) {
                do TakeIt();
                while (IsLetter(_source.Current) || IsDigit(_source.Current));
                return TokenKind.Identifier;
            }

            // if its a digit also add next digita to current token
            if (IsDigit(_source.Current)) {
                do TakeIt();
                while (IsDigit(_source.Current));
                return TokenKind.IntLiteral;
            }

            // if it's an operator take it
            if (IsOperator(_source.Current)) {
                do TakeIt();
                while (IsOperator(_source.Current));
                return TokenKind.Operator;
            }

            // if it's a ' take it and tek next and check if another ' is present
            if (_source.Current == '\'') {
                TakeIt(); TakeIt();
                if (_source.Current == '\'') {
                    TakeIt();
                    return TokenKind.CharLiteral;
                }
                else {
                    // if there is an unclosed char or more than one char show error
					Location ErrPos = _source.getLocation();
                    ErrorReporter.ReportError("Error unterminated char literal at position: " + ErrPos);
                    do TakeIt(); // take characters unitl end of file or closing '
                    while (_source.Current != '\'' && _source.Current != -1);
                    TakeIt(); // take closing '
                    return TokenKind.Error; 
                }
            }

            // check if it's a : and if it's followed by =
            if (_source.Current == ':') {
                TakeIt(); // take colon
                if (_source.Current == '=') {
                    TakeIt(); //take equals
                    return TokenKind.Becomes;
                }
                return TokenKind.Colon;
            }

            // check if current is a symbol and return appropaite token kind
            if (Utils.simbols.ContainsKey((char)_source.Current)) {
                TokenKind k = Utils.simbols[(char)_source.Current];
                TakeIt();
                return k;
            }
            // if program reaches this stage it 
            // means that we have an unrecognized token
            else {
                TakeIt();
				Location ErrPos = _source.getLocation();
                ErrorReporter.ReportError("Error unexpected token at position: " + ErrPos);
                return TokenKind.Error;
            }

        }

        bool IsLetter(int ch) {
            return ('a' <= ch && ch <= 'z') || ('A' <= ch && ch <= 'Z');
        }

        bool IsDigit(int ch) {
            return '0' <= ch && ch <= '9';
        }

        bool IsOperator(int ch) {
            // check if ch is in list of operators
            return (Array.IndexOf(Utils.operators, (char)ch) > -1);
        }
    }
}