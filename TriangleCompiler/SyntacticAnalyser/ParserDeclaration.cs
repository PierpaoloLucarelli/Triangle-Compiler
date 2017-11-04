using System;
using System.Collections.Generic;
namespace TriangleCompiler.SyntacticAnalyser
{
    public partial class Parser
    {
        public void ParseDeclaration()
        {
            System.Console.WriteLine("parsing declaration");
            ParseSingleDeclaration();
            while (_currentToken.Kind == TokenKind.Semicolon)
            {
                AcceptIt();
                ParseSingleDeclaration();
            }
        }

        void ParseSingleDeclaration()
        {
            System.Console.WriteLine("parsing single declaration");
            switch (_currentToken.Kind)
            {
                case TokenKind.Const:
                    {
                        AcceptIt();
                        ParseIdentifier();
                        Accept(TokenKind.Is);
                        ParseExpression();
                        break;
                    }
                case TokenKind.Var:
                    {
                        AcceptIt();
                        ParseIdentifier();
                        if(_currentToken.Kind == TokenKind.Colon)
                        {
                            AcceptIt();
                            ParseTypeDenoter();
                        }
                        else
                            System.Console.WriteLine("ERROR: error in parsing single declaration");
                        break;

                    }
                default:
                    {
                        System.Console.WriteLine("ERROR: error in parsing single declaration");
                        break;
                    }
            }
        }
    }
}
