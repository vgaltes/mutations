namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil.Cil;

    public class BooleanMutator : IMutator
    {
        public bool CanHandle(IEnumerable<Instruction> instructions)
        {
            var booleanOpCodes = new[] { OpCodes.Cgt };

            return instructions.Any(i => booleanOpCodes.Contains(i.OpCode));
        }
    }
}