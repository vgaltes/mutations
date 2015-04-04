namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using Mono.Cecil.Cil;

    public class ArithmeticMutator : Mutator
    {
        public ArithmeticMutator(ITestRunner testRunner) : base(testRunner)
        {
        }

        protected override string GetName()
        {
            return "ArithmeticMutator";
        }

        protected override Dictionary<OpCode, IEnumerable<OpCode>> Mutations
        {
            get
            {
                return new Dictionary<OpCode, IEnumerable<OpCode>>
                {
                    {OpCodes.Add, new[] {OpCodes.Sub, OpCodes.Mul, OpCodes.Div, OpCodes.Rem}},
                    {OpCodes.Sub, new[] {OpCodes.Add, OpCodes.Mul, OpCodes.Div, OpCodes.Rem}},
                    {OpCodes.Div, new[] {OpCodes.Add, OpCodes.Mul, OpCodes.Sub, OpCodes.Rem}}
                };
            }
        }
    }
}