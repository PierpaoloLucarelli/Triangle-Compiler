using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Vnames;

namespace Triangle.Compiler.CodeGenerator.Entities
{
	public class KnownValue : RuntimeEntity, IFetchableEntity
	{
        // entities with known value
		readonly int _value;

		public KnownValue(int size, int value)
			: base(size)
		{
			_value = value;
		}

        // load a literal value onto the stack passing the value directly
		public void EncodeFetch(Emitter emitter, Frame frame, int size, Vname vname)
		{
			emitter.Emit(OpCode.LOADL, 0, 0, _value);
		}
	}
}