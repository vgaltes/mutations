namespace VGA.Mutations.Tests.Mutators
{
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class BooleanMutator
    {
        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAGT_ShouldUseTheBooleanMutator()
        {
            new Mutation().Create<BooleanMachine>()
                .For("GreaterThan")
                .Run()
                .MutatorsUsed.Should()
                .ContainSingle("BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsALT_ShouldUseTheBooleanMutator()
        {
            new Mutation().Create<BooleanMachine>()
                .For("LessThan")
                .Run()
                .MutatorsUsed.Should()
                .ContainSingle("BooleanMutator");
        }
    }
}