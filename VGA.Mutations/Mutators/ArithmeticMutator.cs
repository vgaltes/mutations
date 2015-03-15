namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil.Cil;

    public class ArithmeticMutator : IMutator
    {
        public bool CanHandle(IEnumerable<Instruction> instructions)
        {
            var arithmeticOpCodes = new[] { OpCodes.Add, OpCodes.Sub, OpCodes.Mul, OpCodes.Div };

            return instructions.Any(i => arithmeticOpCodes.Contains(i.OpCode));
        }
    }
}