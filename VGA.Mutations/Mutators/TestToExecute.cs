namespace VGA.Mutations.Mutators
{
    public class TestToExecute
    {
        public TestToExecute(string assemblyName, string className, string methodName)
        {
            AssemblyName = assemblyName;
            MethodName = methodName;
            ClassName = className;
        }
        public string AssemblyName { get; private set; }

        public string MethodName { get; private set; }
        public string ClassName { get; private set; }
    }
}
