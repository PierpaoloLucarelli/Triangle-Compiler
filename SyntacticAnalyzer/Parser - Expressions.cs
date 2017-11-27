
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Actuals;
using Triangle.Compiler.SyntaxTrees.Vnames;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {
        ///////////////////////////////////////////////////////////////////////////////
        //
        // EXPRESSIONS
        //
        ///////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Parses the expression, and constructs an AST to represent its phrase
        /// structure.
        /// </summary>
        /// <returns>
        /// an <link>Triangle.SyntaxTrees.Expressions.Expression</link>
        /// </returns> 
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Expression ParseExpression()
        {
            Expression expression = null;

            var startLocation = _currentToken.Start;

            switch (_currentToken.Kind)
            {

                case TokenKind.Let:
                    {
                        AcceptIt();
                        Declaration declaration = ParseDeclaration();
                        Accept(TokenKind.In);
                        Expression exp = ParseExpression();
                        var expressionPos = new SourcePosition(startLocation, _currentToken.Finish);
                        expression = new LetExpression(declaration, exp, expressionPos);
                        break;
                    }

                case TokenKind.If:
                    {
                        AcceptIt();
                        Expression exp1 = ParseExpression();
                        Accept(TokenKind.Then);
                        Expression exp2 = ParseExpression();
                        Accept(TokenKind.Else);
                        Expression exp3 = ParseExpression();
                        var expressionPos = new SourcePosition(startLocation, _currentToken.Finish);
                        expression = new IfExpression(exp1, exp2, exp3, expressionPos);
                        break;
                    }

                default:
                    {
                        expression = ParseSecondaryExpression();
                        break;
                    }
            }
            return expression;
        }

        /// <summary>
        // Parses the secondary expression, and constructs an AST to represent its
        /// phrase structure.
        /// </summary>
        /// <returns>
        /// an <link>Triangle.SyntaxTrees.Expressions.Expression</link>
        /// </returns>
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Expression ParseSecondaryExpression()
        {
            Expression expression = null;
            var startLocation = _currentToken.Start;
            expression = ParsePrimaryExpression();
            while (_currentToken.Kind == TokenKind.Operator)
            {
                Operator op = ParseOperator();
                Expression exp2 = ParsePrimaryExpression();
                var expressionPos = new SourcePosition(startLocation, _currentToken.Finish);
                expression = new BinaryExpression(expression, op, exp2, expressionPos);
                break;
            }
            return expression;
        }

        /// <summary>
        /// Parses the primary expression, and constructs an AST to represent its
        /// phrase structure.
        /// </summary>
        /// <returns>
        /// an <link>Triangle.SyntaxTrees.Expressions.Expression</link>
        /// </returns>
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Expression ParsePrimaryExpression()
        {
            Expression expression = null;
            var startlocation = _currentToken.Start;
            switch (_currentToken.Kind)
            {

                case TokenKind.IntLiteral:
                    {
                        IntegerLiteral intLit = ParseIntegerLiteral();
                        var expressionPos = new SourcePosition(startlocation, _currentToken.Finish);
                        expression = new IntegerExpression(intLit, expressionPos);
                        break;
                    }

                case TokenKind.CharLiteral:
                    {
                        CharacterLiteral charLit = ParseCharacterLiteral();
                        var expressionPos = new SourcePosition(startlocation, _currentToken.Finish);
                        expression = new CharacterExpression(charLit, expressionPos);
                        break;
                    }


                case TokenKind.Identifier:
                    {
                        Identifier identifier = ParseIdentifier();
                        if (_currentToken.Kind == TokenKind.LeftParen)
                        {
                            AcceptIt();
                            ActualParameterSequence actualParamSeq = ParseActualParameterSequence();
                            Accept(TokenKind.RightParen);
                            var expressionPos = new SourcePosition(startlocation, _currentToken.Finish);
                            expression = new CallExpression(identifier, actualParamSeq, expressionPos);
                            break;
                        }
                        else
                        {
                            var expressionPos = new SourcePosition(startlocation, _currentToken.Finish);
                            SimpleVname svname = new SimpleVname(identifier,expressionPos);
                            expression = new VnameExpression(svname, expressionPos);
                            break;
                        }
                    }

                case TokenKind.Operator:
                    {
                        Operator op = ParseOperator();
                        Expression exp = ParsePrimaryExpression();
                        var expressionPos = new SourcePosition(startlocation, _currentToken.Finish);
                        expression = new UnaryExpression(op, exp, expressionPos);
                        break;
                    }

                case TokenKind.LeftParen:
                    {
                        AcceptIt();
                        expression = ParseExpression();
                        Accept(TokenKind.RightParen);
                        break;
                    }

                default:
                    {
                        RaiseSyntacticError("\"%\" cannot start an expression", _currentToken);
                        break;
                    }
            }
            return expression;
        }

    }
}