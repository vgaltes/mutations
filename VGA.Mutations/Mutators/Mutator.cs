namespace VGA.Mutations.Mutators
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Mono.Cecil;
    using Mono.Cecil.Cil;

    public class MethodToMutate
    {
        public MethodToMutate(string assemblyPath, Type typeToMutate, string methodName)
        {
            AssemblyPath = assemblyPath;
            TypeToMutate = typeToMutate;
            MethodName = methodName;
        }

        public string AssemblyPath { get; private set; }
        public Type TypeToMutate { get; private set; }
        public string MethodName { get; private set; }
    }

    public abstract class Mutator
    {
        private readonly ITestRunner _testRunner;

        protected Mutator(ITestRunner testRunner)
        {
            _testRunner = testRunner;
        }

        protected abstract string Name { get; }

        protected abstract Dictionary<OpCode, IEnumerable<OpCode>> Mutations { get; }

        public MutationResult Mutate(MethodToMutate methodToMutate)
        {
            var testToExecute = new TestDiscoverer().DiscoverTestsToExecute(methodToMutate.AssemblyPath,
                methodToMutate.TypeToMutate.FullName, methodToMutate.MethodName);

            var instructionOffsets = GetInstructionsToMutate(methodToMutate);

            var result = PerformMutations(instructionOffsets, methodToMutate, testToExecute);
            
            return new MutationResult
            {
                MutationsPerformed = result,
                MutatorsUsed = new List<string> {Name}
            };
        }

        private IEnumerable<InstructionToMutate> GetInstructionsToMutate(MethodToMutate methodToMutate)
        {
            var assembly = AssemblyDefinition.ReadAssembly(methodToMutate.AssemblyPath);
            var methodDefinition =
                assembly.MainModule.GetType(methodToMutate.TypeToMutate.FullName).Methods.First(m => m.Name == methodToMutate.MethodName);

            var instructionOffsets =
                methodDefinition.Body.Instructions.Where(instruction => Mutations.ContainsKey(instruction.OpCode))
                    .Select(instruction => new InstructionToMutate(instruction.Offset, instruction.OpCode))
                    .ToList();
            return instructionOffsets;
        }

        private List<string> PerformMutations(IEnumerable<InstructionToMutate> instructionsToMutate, MethodToMutate methodToMutate,
            List<TestToExecute> testsToExecute)
        {
            var result = new List<string>();

            SaveOriginalAssembly(methodToMutate.AssemblyPath);

            foreach (var instructionToMutate in instructionsToMutate)
            {
                result.AddRange(MutateInstruction(instructionToMutate, methodToMutate,
                    testsToExecute));
            }
            
            return result;
        }

        private IEnumerable<string> MutateInstruction(InstructionToMutate instructionToMutate, MethodToMutate methodToMutate, List<TestToExecute> testsToExecute)
        {
            var assembly = AssemblyDefinition.ReadAssembly(GetOriginalAssemblyPath(methodToMutate.AssemblyPath));

            var instruction = GetInstruction(methodToMutate.TypeToMutate, methodToMutate.MethodName, assembly, instructionToMutate);

            var mutations = Mutations[instructionToMutate.OpCode];

            foreach (var opCode in mutations)
            {
                instruction.OpCode = opCode;

                assembly.Write(methodToMutate.AssemblyPath);

                _testRunner.RunTests(testsToExecute, Path.GetDirectoryName(methodToMutate.AssemblyPath));
            }

            return mutations.Select(m => m.Name);
        }

        private static void SaveOriginalAssembly(string assemblyPath)
        {
            var originalAssemblyPath = GetOriginalAssemblyPath(assemblyPath);
            File.Copy(assemblyPath, originalAssemblyPath, true);
        }

        private static string GetOriginalAssemblyPath(string assemblyPath)
        {
            return assemblyPath.ToLowerInvariant().Replace(".dll", "_original.dll");
        }

        private static Instruction GetInstruction(Type typeToMutate, string methodName, AssemblyDefinition assembly,
            InstructionToMutate instructionToMutate)
        {
            var instruction = assembly.MainModule.GetType(typeToMutate.FullName).Methods
                .First(m => m.Name == methodName).Body.Instructions
                .First(i => i.Offset == instructionToMutate.Offset);
            return instruction;
        }

        private class InstructionToMutate
        {
            public InstructionToMutate(int offset, OpCode opCode)
            {
                Offset = offset;
                OpCode = opCode;
            }

            public int Offset { get; private set; }

            public OpCode OpCode { get; private set; }
        }
    }
}