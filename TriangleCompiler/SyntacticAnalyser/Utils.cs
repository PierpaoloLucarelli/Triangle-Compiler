using System.Collections.Generic;
namespace TriangleCompiler.SyntacticAnalyser
{
    public class Utils
    {
		public static readonly Dictionary<char, TokenKind> simbols = new Dictionary<char, TokenKind>(){
			{'.',  TokenKind.Dot},
			{';',  TokenKind.Semicolon},
			{',',  TokenKind.Comma},
			{'~',  TokenKind.Is},
			{'(',  TokenKind.LeftParen},
			{')',  TokenKind.RightParen},
			{'[',  TokenKind.LeftBracket},
			{']',  TokenKind.RightBracket},
			{'{',  TokenKind.LeftCurly},
			{'}',  TokenKind.RightCurly},
		};

		public static char[] operators = { '+', '-', '*', '/', '=', '<', '>', '\\', '&', '@', '%', '^', '?' };
    }
}
