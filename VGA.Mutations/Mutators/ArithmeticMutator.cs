namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using Mono.Cecil.Cil;

    public class ArithmeticMutator : Mutator
    {
        protected override string Name
        {
            get { return "ArithmeticMutator"; }
        }

        protected override Dictionary<OpCode, IEnumerable<OpCode>> Mutations
        {
            get
            {
                return new Dictionary<OpCode, IEnumerable<OpCode>>
                {
                    {OpCodes.Add, new[] {OpCodes.Sub, OpCodes.Mul, OpCodes.Div, OpCodes.Rem}}
                };
            }
        }
    }
}