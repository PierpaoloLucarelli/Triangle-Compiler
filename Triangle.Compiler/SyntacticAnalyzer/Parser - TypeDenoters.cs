/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using Triangle.Compiler.SyntaxTrees.Types;
using Triangle.Compiler.SyntaxTrees.Terminals;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        // /////////////////////////////////////////////////////////////////////////////
        //
        // TYPE-DENOTERS
        //
        // /////////////////////////////////////////////////////////////////////////////

        /**
         * Parses the type denoter, and constructs an AST to represent its phrase
         * structure.
         * 
         * @return a {@link triangle.compiler.syntax.trees.types.TypeDenoter}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        TypeDenoter ParseTypeDenoter()
        {
            TypeDenoter typeDenoter = null;
            var startLocation = _currentToken.Start;
            switch (_currentToken.Kind)
            {

                case TokenKind.Identifier:
                    {
                        Identifier identifier = ParseIdentifier();
                        var typePosition = new SourcePosition(startLocation, _currentToken.Finish);
                        typeDenoter = new SimpleTypeDenoter(identifier, typePosition); // ask this
                        break;
                    }

              
                default:
                    {
                        RaiseSyntacticError("\"%\" cannot start a type denoter", _currentToken);
                        break;
                    }
            }
            return typeDenoter;
        }

    }
}