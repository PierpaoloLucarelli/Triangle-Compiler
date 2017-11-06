using System.Collections.Generic;
namespace TriangleCompiler.SyntacticAnalyser
{
	public class Utils
	{
		// dictionary to map simbols to their respective char
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

		// list of operators
		public static char[] operators = { '+', '-', '*', '/', '=', '<', '>', '\\', '&', '@', '%', '^', '?' };
	}
}
