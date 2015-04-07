using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using VGA.Mutations.Mutators;

namespace VGA.Mutations
{
    public class MutationRunner
    {
        private readonly string _methodName;
        private readonly List<Mutator> _mutators;
        private readonly Type _typeToMutate;

        private string _assemblyPath;
        private ITestRunner _testRunner;

        public MutationRunner(Type typeToMutate, string methodName)
        {
            _typeToMutate = typeToMutate;
            _methodName = methodName;
            _assemblyPath = new Uri(_typeToMutate.Assembly.CodeBase).LocalPath;

            _mutators = new List<Mutator>
            {
                new ArithmeticMutator(),
                new BooleanMutator()
            };

            _testRunner = new NUnitTestRunner();
        }

        public MutationRunner WithTestRunner(ITestRunner testRunner)
        {
            _testRunner = testRunner;
            return this;
        }

        public List<MutationResult> Run()
        {
            string tempPath = CopyFilesToTemporaryFolder();

            _assemblyPath = Path.Combine(tempPath, Path.GetFileName(_assemblyPath));

            var mutationResults = new List<MutationResult>();
            var methodToMutate = new MethodToMutate(_assemblyPath, _typeToMutate, _methodName);

            _mutators.ForEach(m => mutationResults.AddRange(m.Mutate(methodToMutate, _testRunner)));

            var result = new StringBuilder();
            mutationResults.ForEach(mr => result.AppendLine(mr.ToString()));

            Debug.Write(result.ToString());

            if (mutationResults.Any(mr => mr.TestResults.Any(tr => tr.Killed == false)))
            {
                throw new MutationTestFailedException(result.ToString(), mutationResults);
            }

            return mutationResults;
        }

        private static string CopyFilesToTemporaryFolder()
        {
            string tempFolder = Guid.NewGuid().ToString();
            string tempPath = Path.Combine(Path.GetTempPath(), tempFolder);
            Directory.CreateDirectory(tempPath);

            foreach (var file in Directory.GetFiles("."))
            {
                File.Copy(file, Path.Combine(tempPath, file));
            }
            return tempPath;
        }
    }

    public class MutationTestFailedException : Exception
    {
        public MutationTestFailedException(string message, List<MutationResult> results) : base(message)
        {
            Results = results;
        }

        public List<MutationResult> Results { get; private set; }
    }
}