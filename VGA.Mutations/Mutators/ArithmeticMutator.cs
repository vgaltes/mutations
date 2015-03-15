namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil.Cil;

    public class ArithmeticMutator : IMutator
    {
        private Dictionary<OpCode, IEnumerable<OpCode>> _mutations = new Dictionary<OpCode, IEnumerable<OpCode>>
        {
            {OpCodes.Add, new []{OpCodes.Sub, OpCodes.Mul, OpCodes.Div, OpCodes.Rem}}
        };

        public bool CanHandle(IEnumerable<Instruction> instructions)
        {
            var arithmeticOpCodes = new[] { OpCodes.Add, OpCodes.Sub, OpCodes.Mul, OpCodes.Div };

            return instructions.Any(i => arithmeticOpCodes.Contains(i.OpCode));
        }

        public List<string> Mutate(IEnumerable<Instruction> instructions)
        {
            var result = new List<string>();

            foreach (var instruction in instructions)
            {
                if (_mutations.ContainsKey(instruction.OpCode))
                {
                    var mutation = _mutations[instruction.OpCode];
                    result.AddRange(mutation.Select(m => m.Name));
                }
            }

            return result;
        }
    }
}