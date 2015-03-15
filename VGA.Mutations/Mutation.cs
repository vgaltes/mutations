namespace VGA.Mutations
{
    using System;
    using System.Reflection;
    using Mono.Cecil;

    public class Mutation
    {
        public AssemblyDefinition Assembly { get; set; }
        public MethodInfo MethodInfo { get; set; }

        public Mutation Create<T>() where T : class
        {
            var fileUri = new Uri(typeof (T).Assembly.CodeBase);
            Assembly = AssemblyDefinition.ReadAssembly(fileUri.LocalPath);
            return this;
        }

        public Mutation For(string methodName)
        {
            return this;
        }
    }
}