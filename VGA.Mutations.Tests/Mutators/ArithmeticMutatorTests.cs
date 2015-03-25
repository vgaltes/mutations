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
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAMult_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Mult")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsASub_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Sub")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Add")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsArithmeticAndBoolean_ShouldUseTheBooleanAndArithmeticMutator()
        {
            var mutationResults = Mutation.For<Calculator>(_testRunner)
                .InMethod("DivWithCheck")
                .Run();

            mutationResults[0].MutatorsUsed.First().Should().Be("ArithmeticMutator");
            mutationResults[1].MutatorsUsed.First().Should().Be("BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldMutateWithSubMultDivAndRem()
        {
            Mutation.For<Calculator>(_testRunner)
                .InMethod("Add")
                .Run()
                .First()
                .MutationsPerformed.Should()
                .Contain( new[]{"sub", "mul", "div", "rem"});
        }
    }
}