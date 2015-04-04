namespace VGA.Mutations.Tests.Mutators
{
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class BooleanMutatorTests
    {
        private readonly ITestRunner _testRunner = A.Dummy<ITestRunner>();

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAGT_ShouldUseTheBooleanMutator()
        {
            Mutation.For<BooleanMachine>(_testRunner)
                .InMethod("GreaterThan")
                .Run()
                .All(m => m.MutatorUsed == "BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsALT_ShouldUseTheBooleanMutator()
        {
            Mutation.For<BooleanMachine>(_testRunner)
                .InMethod("LessThan")
                .Run()
                .All(m => m.MutatorUsed == "BooleanMutator");
        }
    }
}