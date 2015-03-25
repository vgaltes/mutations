namespace VGA.Mutations
{
    public static class Mutation
    {
        public static MutationClass For<T>() where T : class
        {
            var typeToMutate = typeof (T);
            return new MutationClass(typeToMutate, new NUnitTestRunner());
        }

        public static MutationClass For<T>(ITestRunner testRunner) where T : class
        {
            var typeToMutate = typeof(T);
            return new MutationClass(typeToMutate, testRunner);
        }
    }
}