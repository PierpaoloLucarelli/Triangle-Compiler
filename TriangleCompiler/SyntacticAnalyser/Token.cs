/* 
 * Pierpaolo Lucarelli - CM4106 - Full Time: Languages and Compilers
 * CM4106 - Full Time: Languages and Compilers
 */

using System;
using System.Collections.Generic;
using System.Linq;

namespace TriangleCompiler.SyntacticAnalyser
{
	public class Token
	{
		// Lookup table of reserved words used to screen tokens.
		static readonly IDictionary<string, TokenKind> ReservedWords =
				Enumerable.Range((int)TokenKind.Array, (int)TokenKind.While).Cast<TokenKind>()
					.ToDictionary(kind => kind.ToString().ToLower(), kind => kind);

		// the position of the token in the file
		public SourcePosition Pos;

		//The kind of a source token.
		public TokenKind Kind { get; private set; }

		//The spelling of a source token.
		public string Spelling { get; private set; }

		// Creates a token
		public Token(TokenKind kind, string spelling, SourcePosition pos)
		{
			Kind = kind;
			this.Pos = pos; // save position
			if (kind == TokenKind.Identifier)
			{
				TokenKind match;
				if (ReservedWords.TryGetValue(spelling, out match))
				{
					Kind = match;
				}
			}
			Spelling = spelling;
		}

		public override string ToString()
		{
			return string.Format("Kind={0}, spelling=\"{1}\" \nat position: {2}", Kind, Spelling, Pos);
		}
	}
}