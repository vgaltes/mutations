namespace VGA.Mutations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Mono.Cecil;
    using Mutators;

    public class Mutation
    {
        private readonly List<IMutator> _mutators; 
        public AssemblyDefinition Assembly { get; set; }
        public MethodDefinition MethodDefinition { get; set; }

        private Type _typeToMutate;

        public Mutation()
        {
            _mutators = new List<IMutator>
            {
                new ArithmeticMutator(),
                new BooleanMutator()
            };
        }

        public Mutation Create<T>() where T : class
        {
            _typeToMutate = typeof (T);
            var fileUri = new Uri(_typeToMutate.Assembly.CodeBase);
            Assembly = AssemblyDefinition.ReadAssembly(fileUri.LocalPath);
            
            return this;
        }

        public Mutation For(string methodName)
        {
            MethodDefinition =
                Assembly.MainModule.GetType(_typeToMutate.FullName).Methods.First(m => m.Name == methodName);
            return this;
        }

        public MutationResult Run()
        {
            var originalFilePath = new Uri(_typeToMutate.Assembly.CodeBase).LocalPath;
            var assemblyMutatedFileName = originalFilePath.ToLowerInvariant().Replace(".dll", "_Mutated.dll");

            var mutatorToUse = DiscoverMutatorsToUse();
            
            Assembly.Write(assemblyMutatedFileName);

            return new MutationResult {MutatorsUsed = mutatorToUse};
        }

        private List<string> DiscoverMutatorsToUse()
        {
            var result = new List<string>();
            result.AddRange(_mutators.Where(m => m.CanHandle(MethodDefinition.Body.Instructions)).Select(m => m.GetType().Name));

            return result;
        }
    }
}