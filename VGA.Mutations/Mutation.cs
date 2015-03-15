namespace VGA.Mutations
{
    public class Mutation
    {
        public AssemblyDefinition Assembly { get; set; }

        public Mutation Create<T>() where T : class
        {
            return this;
        }
    }

    public class AssemblyDefinition
    {
    }
}