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
            ParseActualParameter();
            while (_currentToken.Kind == TokenKind.Comma)
            {
                AcceptIt();
                ParseActualParameter();
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
                default:
                    {
                        ParseExpression();
                        break;
                    }
            }            
        } 
    }
}
