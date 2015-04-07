namespace VGA.Mutations
{
    using System;

    public class MutationClass
    {
        private readonly Type _typeToMutate;

        public MutationClass(Type typeToMutate)
        {
            _typeToMutate = typeToMutate;
        }

        public MutationRunner InMethod(string method)
        {
            return new MutationRunner(_typeToMutate, method);
        }
    }
}