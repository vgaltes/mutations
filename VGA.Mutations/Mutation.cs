namespace VGA.Mutations
{
    using System;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class Mutation
    {
        
        public AssemblyDefinition Assembly { get; set; }
        public MethodDefinition MethodDefinition { get; set; }

        private Type TypeToMutate { get; set; }

        public Mutation Create<T>() where T : class
        {
            TypeToMutate = typeof (T);
            var fileUri = new Uri(TypeToMutate.Assembly.CodeBase);
            Assembly = AssemblyDefinition.ReadAssembly(fileUri.LocalPath);
            
            return this;
        }

        public Mutation For(string methodName)
        {
            MethodDefinition =
                Assembly.MainModule.GetType(TypeToMutate.FullName).Methods.First(m => m.Name == methodName);
            return this;
        }

        public MutationResult Run()
        {
            var originalFilePath = new Uri(TypeToMutate.Assembly.CodeBase).LocalPath;
            var assemblyMutatedFileName = originalFilePath.ToLowerInvariant().Replace(".dll", "_Mutated.dll");

            var mutatorToUse = DiscoverMutatorsToUse();
            
            Assembly.Write(assemblyMutatedFileName);

            return new MutationResult {MutatorUsed = mutatorToUse};
        }

        private string DiscoverMutatorsToUse()
        {
            var mutatorToUse = "";
            var arithmeticOpCodes = new[] {OpCodes.Add, OpCodes.Sub, OpCodes.Mul, OpCodes.Div};
            var booleanOpCodes = new[] {OpCodes.Cgt};
            if (MethodDefinition.Body.Instructions.Any(i => arithmeticOpCodes.Contains(i.OpCode)))
            {
                mutatorToUse = "ArithmeticMutator";
            }
            else if (MethodDefinition.Body.Instructions.Any(i => booleanOpCodes.Contains(i.OpCode)))
            {
                mutatorToUse = "BooleanMutator";
            }

            return mutatorToUse;
        }
    }
}