namespace VGA.Mutations.Mutators
{
    using System.Collections.Generic;
    using Mono.Cecil.Cil;

    public class BooleanMutator : Mutator
    {
        protected override string Name
        {
            get { return "BooleanMutator"; }
        }

        protected override Dictionary<OpCode, IEnumerable<OpCode>> Mutations
        {
            get { return new Dictionary<OpCode, IEnumerable<OpCode>>
                    {
                        {OpCodes.Cgt, new []{OpCodes.Clt}}
                    }; 
            }
        }
    }
}