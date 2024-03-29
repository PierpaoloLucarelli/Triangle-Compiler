/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using Triangle.Compiler.SyntaxTrees;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        ///////////////////////////////////////////////////////////////////////////////
        //
        // PROGRAMS
        //
        ///////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Parses a Triangle program, and constructs an AST to represent it.
        /// </summary>
        /// <returns>
        /// a {@link triangle.compiler.syntax.trees.Program} or null if there
        /// is a syntactic error
        /// </returns>
        public Program ParseProgram()
        {
            try
            {
                // parse the program
                _tokens.MoveNext();
                _currentToken = _tokens.Current;
                var startLocation = _currentToken.Start;
                var command = ParseCommand();
                var pos = new SourcePosition(startLocation, _currentToken.Finish);
                var program = new Program(command, pos);
                if (_currentToken.Kind != TokenKind.EndOfText)
                {
                    RaiseSyntacticError("\"%\" not expected after end of program", _currentToken);
                }
                return program;

            }
            catch (SyntaxError)
            {
                return null;
            }
        }
    }
}