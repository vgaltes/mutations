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
            Mutation.For<BooleanMachine>()
                .InMethod("GreaterThan")
                .WithTestRunner(_testRunner)
                .Run()
                .Any(m => m.MutatorUsed == "BooleanMutator")
                .Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsALT_ShouldUseTheBooleanMutator()
        {
            Mutation.For<BooleanMachine>()
                .InMethod("LessThan")
                .WithTestRunner(_testRunner)
                .Run()
                .Any(m => m.MutatorUsed == "BooleanMutator")
                .Should().BeTrue();
        }
    }
}