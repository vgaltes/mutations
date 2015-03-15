namespace VGA.Mutations
{
    using System;
    using System.Collections.Generic;
    using Mutators;

    public class MutationRunner
    {
        private readonly string _methodName;

        private readonly List<Mutator> _mutators;
        private readonly Type _typeToMutate;

        private string _assemblyPath;

        public MutationRunner(Type typeToMutate, string methodName)
        {
            _typeToMutate = typeToMutate;
            _methodName = methodName;

            _mutators = new List<Mutator>
            {
                new ArithmeticMutator(),
                new BooleanMutator()
            };
        }

        public List<MutationResult> Run()
        {
            _assemblyPath = new Uri(_typeToMutate.Assembly.CodeBase).LocalPath;

            var mutationResults = new List<MutationResult>();

            _mutators.ForEach(m => mutationResults.Add(m.Mutate(_assemblyPath, _typeToMutate, _methodName)));

            return mutationResults;
        }
    }
}