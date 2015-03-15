namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using Mono.Cecil.Cil;

    internal interface IMutator
    {
        bool CanHandle(IEnumerable<Instruction> instructions);
    }
}