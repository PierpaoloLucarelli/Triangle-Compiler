using Triangle.Compiler.SyntaxTrees.Declarations;
using Triangle.Compiler.SyntaxTrees.Visitors;

namespace Triangle.Compiler.ContextualAnalyzer
{
    public partial class Checker : IDeclarationVisitor
    {
        

        public Void VisitConstDeclaration(ConstDeclaration ast, Void arg)
        {
            ast.Expression.Visit(this);
            _idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared",ast.Identifier, ast);
            return null;
        }

		public Void VisitVarDeclaration(VarDeclaration ast, Void arg)
		{
			//  Visit declaration and add the type to the declaration
			ast.Type = ast.Type.Visit(this);
            _idTable.Enter(ast.Identifier, ast);
            CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared", ast.Identifier, ast);
			return null;
		}

        public Void VisitSequentialDeclaration(SequentialDeclaration ast, Void arg)
        {
            ast.FirstDeclaration.Visit(this);
            ast.SecondDeclaration.Visit(this);
            return null;
        }

		public Void VisitTypeDeclaration(TypeDeclaration ast, Void arg)
		{
			//  Visit declaration and add the type to the declaration
			ast.Type = ast.Type.Visit(this);
			_idTable.Enter(ast.Identifier, ast);
			CheckAndReportError(!ast.Duplicated, "identifier \"%\" already declared",
				ast.Identifier, ast);
			return null;
		}

        public Void VisitUnaryOperatorDeclaration(UnaryOperatorDeclaration ast, Void arg)
        {
            return null;
        }


		public Void VisitBinaryOperatorDeclaration(BinaryOperatorDeclaration ast, Void arg)
		{
			return null;
		}



    }
}