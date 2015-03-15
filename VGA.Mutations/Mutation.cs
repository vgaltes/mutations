namespace VGA.Mutations
{
    using System;
    using System.Linq;
    using Mono.Cecil;

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
    }
}