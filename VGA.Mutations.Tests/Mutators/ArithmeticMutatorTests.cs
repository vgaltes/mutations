using System.Diagnostics;

namespace VGA.Mutations.Tests.Mutators
{
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class ArithmeticMutatorTests
    {
        private readonly ITestRunner _testRunner = A.Dummy<ITestRunner>();

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsADiv_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Div")
                .Run()
                .All(m => m.MutatorUsed == "ArithmeticMutator");

        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAMult_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Mult")
                .Run()
                .All(m => m.MutatorUsed == "ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsASub_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Sub")
                .Run()
                .All(m => m.MutatorUsed == "ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Add")
                .Run()
                .All(m => m.MutatorUsed == "ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsArithmeticAndBoolean_ShouldUseTheBooleanAndArithmeticMutator()
        {
            var mutationResults = Mutation.For<Calculator>(_testRunner)
                .InMethod("DivWithCheck")
                .Run();

            mutationResults.Count(mr => mr.MutatorUsed == "ArithmeticMutator").Should().Be(8);
            mutationResults.Count(mr => mr.MutatorUsed == "BooleanMutator").Should().Be(1);
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldMutateWithSubMultDivAndRem()
        {
            var mutationResults = Mutation.For<Calculator>(_testRunner)
                .InMethod("Add")
                .Run();

            mutationResults.Any(mr => mr.MutationPerformed.Contains("sub")).Should().BeTrue();
            mutationResults.Any(mr => mr.MutationPerformed.Contains("mul")).Should().BeTrue();
            mutationResults.Any(mr => mr.MutationPerformed.Contains("div")).Should().BeTrue();
            mutationResults.Any(mr => mr.MutationPerformed.Contains("rem")).Should().BeTrue();
        }

        [Test]
        [ExpectedException(typeof(MutationTestFailedException))]
        public void WhenMutatingAdd_ShouldSurviveOneTest()
        {
            Mutation.For<Calculator>(new NUnitTestRunner())
                .InMethod("Add")
                .Run();
        }
    }
}