using Triangle.AbstractMachine;
using Triangle.Compiler.SyntaxTrees.Vnames;

namespace Triangle.Compiler.CodeGenerator.Entities
{
	public class UnknownAddress : AddressableEntity
	{
        // entities with unknwon address
		public UnknownAddress(int size, int level, int displacement)
			: base(size, level, displacement)
		{
		}

        // push the object to at the address created from the Annotated AST to the top of the stack
		public override void EncodeAssign(Emitter emitter, Frame frame, int size, Vname vname)
		{
			emitter.Emit(OpCode.LOAD, Machine.AddressSize, frame.DisplayRegister(_address),
 _address.Displacement);
            // pop that address and retrieve its objects and store it
			emitter.Emit(OpCode.STOREI, size, 0, 0);
		}

        // push on object to the top of the stack, then use the address to fetch an object.
        // then oush the object ontop of the stack 
		public override void EncodeFetch(Emitter emitter, Frame frame, int size, Vname vname)
		{
			emitter.Emit(OpCode.LOAD, Machine.AddressSize, frame.DisplayRegister(_address));
			emitter.Emit(OpCode.STORE, size);
		}

        // load an address on top of the stack 
		public override void EncodeFetchAddress(Emitter emitter, Frame frame, Vname vname)
		{
			emitter.Emit(OpCode.LOAD, Machine.AddressSize, frame.DisplayRegister(_address), _address.Displacement);
		}

	}
}