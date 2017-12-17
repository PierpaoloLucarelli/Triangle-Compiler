/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using Triangle.Compiler.SyntaxTrees.Commands;

namespace Triangle.Compiler.SyntacticAnalyzer
{
    public partial class Parser
    {
        ///////////////////////////////////////////////////////////////////////////////
        //
        // COMMANDS
        //
        ///////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Parses the command, and constructs an AST to represent its phrase
        /// structure.
        /// </summary>
        /// <returns>
        /// a <link>Triangle.SyntaxTrees.Commands.Command</link>
        /// </returns>
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Command ParseCommand()
        {
            // command is composed of single comand followed by other single comands
            // seprated by a ;
            var startLocation = _currentToken.Start;
            var command = ParseSingleCommand();
            while (_currentToken.Kind == TokenKind.Semicolon)
            {
                AcceptIt();
                var command2 = ParseSingleCommand();
                var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                // if there is more than one command, return sequential comand
                command = new SequentialCommand(command, command2, commandPosition);
            }
            return command;
        }

        /// <summary>
        /// Parses the single command, and constructs an AST to represent its phrase
        /// structure.
        /// </summary>
        /// <returns>
        /// a {@link triangle.compiler.syntax.trees.commands.Command}
        /// </returns>
        /// <throws type="SyntaxError">
        /// a syntactic error
        /// </throws>
        Command ParseSingleCommand()
        {

            var startLocation = _currentToken.Start;

            switch (_currentToken.Kind)
            {

                case TokenKind.Identifier:
                    {
                        var identifier = ParseIdentifier();
                        // call command
                        if (_currentToken.Kind == TokenKind.LeftParen)
                        {
                            AcceptIt(); // take become token
							var actuals = ParseActualParameterSequence();
                            Accept(TokenKind.RightParen);
                            var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                            return new CallCommand(identifier, actuals, commandPosition);
                        }
                        // assign command
                        else
                        {
                            var vname = ParseRestOfVname(identifier);
                            Accept(TokenKind.Becomes);
                            var expression = ParseExpression();
                            var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                            return new AssignCommand(vname, expression, commandPosition);
                        }
                    }

                case TokenKind.Begin:
                    {
                        AcceptIt(); // take begin token
						var command = ParseCommand();
                        Accept(TokenKind.End);
                        return command;
                    }
                case TokenKind.Let:
                    {
						// let command
						AcceptIt(); // take let token
						var declaration = ParseDeclaration();
                        Accept(TokenKind.In);
                        var command = ParseSingleCommand();
                        var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        return new LetCommand(declaration, command, commandPosition);
                    }

                case TokenKind.If:
                    {
                        // If command
                        AcceptIt(); // take if token
						var expression = ParseExpression();
                        Accept(TokenKind.Then);
                        var command1 = ParseSingleCommand();
                        Accept(TokenKind.Else);
                        var command2 = ParseSingleCommand();
                        var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        return new IfCommand(expression, command1, command2, commandPosition);
                    }

                case TokenKind.While:
                    {
                        // While command
                        AcceptIt(); // take while token
						var expression = ParseExpression();
                        Accept(TokenKind.Do);
                        var command = ParseSingleCommand();
                        var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        return new WhileCommand(expression, command, commandPosition);
                    }
				// fix the trailing else problem    
				case TokenKind.Semicolon:
                case TokenKind.End:
                case TokenKind.Else:
                case TokenKind.In:
                case TokenKind.EndOfText:
                    {
                        var commandPosition = new SourcePosition(startLocation, _currentToken.Finish);
                        return new EmptyCommand(commandPosition);
                    }

                default:
                    RaiseSyntacticError("\"%\" cannot start a command", _currentToken);
                    return null;
            }
        }
    }
}