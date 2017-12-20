/*
 * Pierpaolo Lucarelli - 1400571 
 * CM4106 Languages and comilers - Final compiler
*/
using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Vnames;

namespace Triangle.Compiler.CodeGenerator.Entities
{
    // entity with known address
	public class KnownAddress : AddressableEntity
	{

		public KnownAddress(int size, int level, int displacement)
			: base(size, level, displacement)
		{
		}

		public KnownAddress(int size, Frame frame)
			 : base(size, frame)
		{
		}

		// assign a value to a variable by popping a value from the top of the stack and storing it 
        // in a variable
		public override void EncodeAssign(Emitter emitter, Frame frame, int size, Vname vname)
		{
			emitter.Emit(OpCode.STORE, size, frame.DisplayRegister(Address), Address.Displacement);
		}

        // load the value from an address onto the stack
		public override void EncodeFetch(Emitter emitter, Frame frame, int size, Vname vname)
		{
			emitter.Emit(OpCode.LOAD, size, frame.DisplayRegister(Address), Address.Displacement);
		}

        // load an address onto the stack
		public override void EncodeFetchAddress(Emitter emitter, Frame frame, Vname vname)
		{
			emitter.Emit(OpCode.LOADA, frame.DisplayRegister(Address), Address.Displacement);
		}
	}
}