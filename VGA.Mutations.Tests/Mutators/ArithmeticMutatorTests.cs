namespace VGA.Mutations.Tests.Mutators
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class ArithmeticMutatorTests
    {
        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsADiv_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Div")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAMult_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Mult")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsASub_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Sub")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Add")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsArithmeticAndBoolean_ShouldUseTheBooleanAndArithmeticMutator()
        {
            var mutationResults = Mutation.For<Calculator>()
                .InMethod("DivWithCheck")
                .Run();

            mutationResults[0].MutatorsUsed.First().Should().Be("ArithmeticMutator");
            mutationResults[1].MutatorsUsed.First().Should().Be("BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldMutateWithSubMultDivAndRem()
        {
            Mutation.For<Calculator>()
                .InMethod("Add")
                .Run()
                .First()
                .MutationsPerformed.Should()
                .Contain( new[]{"sub", "mul", "div", "rem"});
        }
    }
}