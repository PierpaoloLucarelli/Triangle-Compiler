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
            ActualParameterSequence ActualParamSeq;
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
            var startLocation = _currentToken.Position.Start;
            ParseActualParameter();
            if (_currentToken.Kind == TokenKind.Comma)
            {
                AcceptIt();
                ParseProperActualParameterSequence();
                var actualsPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);

            }
            else
            {
                var actualsPosition = new SourcePosition(startLocation, _currentToken.Position.Finish);

            }
           
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
            return actualParam;

        }
    }
}