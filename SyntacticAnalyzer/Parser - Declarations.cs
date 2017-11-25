using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Terminals;
using Triangle.Compiler.SyntaxTrees.Expressions;
using Triangle.Compiler.SyntaxTrees.Types;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {

        ///////////////////////////////////////////////////////////////////////////////
        //
        // DECLARATIONS
        //
        ///////////////////////////////////////////////////////////////////////////////

        /**
         * Parses the declaration, and constructs an AST to represent its phrase
         * structure.
         * 
         * @return a {@link triangle.compiler.syntax.trees.declarations.Declaration}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        Declaration ParseDeclaration()
        {
            Declaration declaration = null;
            var startLocation = _currentToken.Start;
            declaration = ParseSingleDeclaration();
            while (_currentToken.Kind == TokenKind.Semicolon)
            {
                AcceptIt();
                Declaration decl = ParseSingleDeclaration();
                var declarationPosition = new SourcePosition(startLocation, _currentToken.Finish);
                declaration = new SequentialDeclaration(declaration, decl, declarationPosition);
            }
            return declaration;
        }

        /**
         * Parses the single declaration, and constructs an AST to represent its
         * phrase structure.
         * 
         * @return a {@link triangle.compiler.syntax.trees.declarations.Declaration}
         * 
         * @throws SyntaxError
         *           a syntactic error
         * 
         */
        Declaration ParseSingleDeclaration()
        {
            Declaration declaration = null;
            var startLocation = _currentToken.Start;
            switch (_currentToken.Kind)
            {

                case TokenKind.Const:
                    {
                        AcceptIt();
                        Identifier identifier = ParseIdentifier();
                        Accept(TokenKind.Is);
                        Expression expression = ParseExpression();
                        var declarationPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        declaration = new ConstDeclaration(identifier, expression, declarationPosition);
                        break;

                    }

                case TokenKind.Var:
                    {
                        AcceptIt();
                        Identifier identifier = ParseIdentifier();
                        Accept(TokenKind.Colon);
                        TypeDenoter typeDenoter = ParseTypeDenoter();
                        var declarationPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        declaration = new VarDeclaration(identifier, typeDenoter, declarationPosition);
                        break;
                    }


                    // is this extra?
                case TokenKind.Type:
                    {
                        AcceptIt();
                        Identifier identifier = ParseIdentifier();
                        Accept(TokenKind.Is);
                        TypeDenoter typeDenoter = ParseTypeDenoter();
                        var declarationPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        declaration = new TypeDeclaration(identifier, typeDenoter, declarationPosition);
                        break;
                    }

                default:
                    {
                        RaiseSyntacticError("\"%\" cannot start a declaration", _currentToken);
                        break;
                    }
            }
            return declaration;

        }
    }
}