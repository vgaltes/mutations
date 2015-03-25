namespace VGA.Mutations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using Mutators;

    public class MutationRunner
    {
        private readonly string _methodName;

        private readonly List<Mutator> _mutators;
        private readonly Type _typeToMutate;

        private string _assemblyPath;

        public MutationRunner(Type typeToMutate, string methodName, ITestRunner testRunner)
        {
            _typeToMutate = typeToMutate;
            _methodName = methodName;
            _assemblyPath = new Uri(_typeToMutate.Assembly.CodeBase).LocalPath;
            
            _mutators = new List<Mutator>
            {
                new ArithmeticMutator(testRunner),
                new BooleanMutator(testRunner)
            };
        }

        public List<MutationResult> Run()
        {
            var tempPath = CopyFilesToTemporaryFolder();

            _assemblyPath = Path.Combine(tempPath, Path.GetFileName(_assemblyPath));

            var mutationResults = new List<MutationResult>();
            var methodToMutate = new MethodToMutate(_assemblyPath, _typeToMutate, _methodName);

            _mutators.ForEach(m => mutationResults.Add(m.Mutate(methodToMutate)));

            return mutationResults;
        }

        private static string CopyFilesToTemporaryFolder()
        {
            var tempFolder = Guid.NewGuid().ToString();
            var tempPath = Path.Combine(Path.GetTempPath(), tempFolder);
            Directory.CreateDirectory(tempPath);

            foreach (var file in Directory.GetFiles("."))
            {
                File.Copy(file, Path.Combine(tempPath, file));
            }
            return tempPath;
        }
    }
}