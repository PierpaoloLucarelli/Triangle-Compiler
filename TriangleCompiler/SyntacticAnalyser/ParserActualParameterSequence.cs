using System;
using System.Collections.Generic;
using System.Text;

namespace TriangleCompiler.SyntacticAnalyser
{
     partial class Parser
    {
        void ParseActualParameterSequence()
        {
            System.Console.WriteLine("parsing actual parameter sequence");
            // if current token if right braket, we don't prase parameter sequence
            if (_currentToken.Kind != TokenKind.RightParen)
            {
                ParseActualParameter();
                while (_currentToken.Kind == TokenKind.Comma)
                {
                    AcceptIt();
                    ParseActualParameter();
                }
            }

        }

        void ParseActualParameter()
        {
            System.Console.WriteLine("parsing actual parameter");
            switch (_currentToken.Kind)
            {
                case (TokenKind.Var):
                    {
                        AcceptIt();
                        ParseVname();
                        break;
                    }

                case TokenKind.Identifier:
                case TokenKind.IntLiteral:
                case TokenKind.CharLiteral:
                case TokenKind.Operator:
                case TokenKind.LeftParen:
                    {
                        ParseExpression();
                        break;
                    }

            }            
        } 
    }
}
