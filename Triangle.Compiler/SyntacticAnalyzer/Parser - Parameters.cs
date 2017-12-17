/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using Triangle.Compiler.SyntaxTrees.Actuals;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Vnames;


namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        ///////////////////////////////////////////////////////////////////////////////
        //
        // PARAMETERS
        //
        ///////////////////////////////////////////////////////////////////////////////

       

       

        /**
         * Parses the actual parameter sequence, and constructs an AST to represent
         * its phrase structure.
         * 
         * @return an
         *         {@link triangle.compiler.syntax.trees.actuals.ActualParameterSequence}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        ActualParameterSequence  ParseActualParameterSequence()
        {
            // empty param seq if there is a right paren, otherwise: proper param sequence
            ActualParameterSequence ActualParamSeq = null;
            var startLocation = _currentToken.Position.Start;
            if (_currentToken.Kind == TokenKind.RightParen)
            {
                var actualsPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);
                ActualParamSeq = new EmptyActualParameterSequence(actualsPosition);
            } else{
                ActualParamSeq = ParseProperActualParameterSequence();
            }
            return ActualParamSeq;
        }

        /**
         * Parses the proper (non-empty) actual parameter sequence, and constructs an
         * AST to represent its phrase structure.
         * 
         * @return an
         *         {@link triangle.compiler.syntax.trees.actuals.ActualParameterSequence}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        ActualParameterSequence ParseProperActualParameterSequence()
        {
            // if there is a more than one parameter, Multiple actual param seq
            // else single param sequence.
            ActualParameterSequence properParamSeq = null;
            var startLocation = _currentToken.Position.Start;
            ActualParameter actualParam = ParseActualParameter();
            if (_currentToken.Kind == TokenKind.Comma) // parameters seprarated by a comma
            {
                AcceptIt(); // take the comma
                ActualParameterSequence actualParamSeq = ParseProperActualParameterSequence();
                var actualsPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);
                properParamSeq = new MultipleActualParameterSequence(actualParam, actualParamSeq, actualsPosition);
            }
            else
            {
                var actualsPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);
                properParamSeq = new SingleActualParameterSequence(actualParam, actualsPosition);
            }
            return properParamSeq;
        }

        /**
         * Parses the actual parameter, and constructs an AST to represent its phrase
         * structure.
         * 
         * @return an {@link triangle.compiler.syntax.trees.actuals.ActualParameter}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        ActualParameter ParseActualParameter()
        {
            ActualParameter actualParam = null;
            var startLocation = _currentToken.Position.Start;
            switch (_currentToken.Kind)
            {
                // all cases for an expression
                case TokenKind.Identifier:
                case TokenKind.IntLiteral:
                case TokenKind.CharLiteral:
                case TokenKind.Operator:
                case TokenKind.Let:
                case TokenKind.If:
                case TokenKind.LeftParen:
                case TokenKind.LeftBracket:
                case TokenKind.LeftCurly:
                    {
                        Expression expression = ParseExpression();
                        var actualPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);
                        actualParam = new ConstActualParameter(expression, actualPosition);
                        break;
                    }

                case TokenKind.Var:
                    {
                        // vaname parameter
                        AcceptIt();
                        Vname vname = ParseVname();
                        var actualPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);
                        actualParam = new VarActualParameter(vname, actualPosition);
                        break;
                    }

                default:
                    {
                        RaiseSyntacticError("\"%\" cannot start an actual parameter", _currentToken);
                        break;
                    }
            }
			// returns null if there is an eror
			return actualParam;
        }
    }
}