/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Vnames;

namespace Triangle.Compiler.CodeGenerator.Entities
{
	public class UnknownValue : RuntimeEntity, IFetchableEntity
	{

		// entities with unknown value
		readonly ObjectAddress _address;

		public UnknownValue(int size, int level, int displacement)
			: base(size)
		{
			_address = new ObjectAddress(level, displacement);
		}

		public UnknownValue(int size, Frame frame)
			:
			this(size, frame.Level, frame.Size)
		{
		}

        // we dont know the value so we can only work with the address.
        // load the value at an address onto the stack
		public void EncodeFetch(Emitter emitter, Frame frame, int size, Vname vname)
		{
			if (vname.IsIndexed)
			{
				emitter.Emit(OpCode.LOAD, frame.DisplayRegister(_address), _address.Displacement);
            } else{
				emitter.Emit(OpCode.LOAD, size, frame.DisplayRegister(_address), _address.Displacement);
            }
		}
	}
}