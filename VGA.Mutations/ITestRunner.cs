namespace VGA.Mutations
{
    using System.Collections.Generic;
    using Mutators;

    public interface ITestRunner
    {
        IEnumerable<TestToExecute> RunTests(IEnumerable<TestToExecute> testsToExecute, string tempPath);
    }
}