namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using Mono.Cecil.Cil;
    using Mono.Collections.Generic;

    internal interface IMutator
    {
        bool CanHandle(IEnumerable<Instruction> instructions);

        List<string> Mutate(IEnumerable<Instruction> instructions);
    }
}