namespace VGA.Mutations
{
    using System;

    public class MutationClass
    {
        private readonly Type _typeToMutate;
        private readonly ITestRunner _testRunner;

        public MutationClass(Type typeToMutate, ITestRunner testRunner)
        {
            _typeToMutate = typeToMutate;
            _testRunner = testRunner;
        }

        public MutationRunner InMethod(string method)
        {
            return new MutationRunner(_typeToMutate, method, _testRunner);
        }
    }
}