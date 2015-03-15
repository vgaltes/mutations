namespace VGA.Mutations.Mutators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public abstract class Mutator
    {
        protected abstract string Name { get; }

        protected abstract Dictionary<OpCode, IEnumerable<OpCode>> Mutations { get; }

        public MutationResult Mutate(string assemblyPath, Type typeToMutate, string methodName)
        {
            var assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
            var methodDefinition =
                assembly.MainModule.GetType(typeToMutate.FullName).Methods.First(m => m.Name == methodName);

            var assemblyMutatedFileName = assemblyPath.ToLowerInvariant().Replace(".dll", "_Mutated.dll");

            var result = new List<string>();

            foreach (var instruction in methodDefinition.Body.Instructions)
            {
                if (Mutations.ContainsKey(instruction.OpCode))
                {
                    var originalOpCode = instruction.OpCode;

                    var mutation = Mutations[instruction.OpCode];
                    foreach (var opCode in mutation)
                    {
                        instruction.OpCode = opCode;
                        assembly.Write(assemblyMutatedFileName);

                        //Pass tests
                    }

                    instruction.OpCode = originalOpCode;
                    result.AddRange(mutation.Select(m => m.Name));
                }
            }


            return new MutationResult
            {
                MutationsPerformed = result,
                MutatorsUsed = new List<string> { Name }
            };
        }
    }
}