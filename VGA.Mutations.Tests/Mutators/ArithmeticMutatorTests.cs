namespace VGA.Mutations.Tests.Mutators
{
    using System.Collections.Generic;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class ArithmeticMutatorTests
    {
        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsADiv_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>()
                .For("Div")
                .Run()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAMult_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>()
                .For("Mult")
                .Run()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsASub_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>()
                .For("Sub")
                .Run()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>()
                .For("Add")
                .Run()
                .MutatorsUsed.Should()
                .ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsArithmeticAndBoolean_ShouldUseTheBooleanAndArithmeticMutator()
        {
            new Mutation().Create<Calculator>()
                .For("DivWithCheck")
                .Run()
                .MutatorsUsed.Should()
                .Contain(new List<string> {"BooleanMutator", "ArithmeticMutator"});
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldMutateWithSubMultDivAndRem()
        {
            new Mutation().Create<Calculator>()
                .For("Add")
                .Run()
                .MutationsPerformed.Should()
                .Contain( new[]{"sub", "mul", "div", "rem"});
        }
    }
}