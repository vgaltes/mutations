namespace VGA.Mutations.Tests.Mutators
{
    using System.Linq;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class BooleanMutatorTests
    {
        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAGT_ShouldUseTheBooleanMutator()
        {
            Mutation.For<BooleanMachine>()
                .InMethod("GreaterThan")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsALT_ShouldUseTheBooleanMutator()
        {
            Mutation.For<BooleanMachine>()
                .InMethod("LessThan")
                .Run()
                .First()
                .MutatorsUsed.Should()
                .ContainSingle("BooleanMutator");
        }
    }
}