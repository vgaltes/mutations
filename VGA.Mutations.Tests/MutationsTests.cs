namespace VGA.Mutations.Tests
{
    using System.Collections.Generic;
    using System.IO;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class MutationsTests
    {
        [Test]
        public void WhenAMutationIsCreated_TheAssemblyShouldBeLoaded()
        {
            new Mutation().Create<Calculator>().Assembly.Should().NotBeNull();
        }

        [Test]
        public void WhenAMethodIsSpecified_TheMethodInfoShouldBeLoaded()
        {
            new Mutation().Create<Calculator>().For("Add").MethodDefinition.Should().NotBeNull();
        }

        [Test]
        public void WhenTheMutationRuns_ShouldCreateANewFile()
        {
            new Mutation().Create<Calculator>().For("Add").Run();

            File.Exists(".\\VGA.Mutations.TestAssembly_Mutated.dll").Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>().For("Add").Run().MutatorsUsed.Should().ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsASub_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>().For("Sub").Run().MutatorsUsed.Should().ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAMult_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>().For("Mult").Run().MutatorsUsed.Should().ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsADiv_ShouldUseTheArithmeticMutator()
        {
            new Mutation().Create<Calculator>().For("Div").Run().MutatorsUsed.Should().ContainSingle("ArithmeticMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAGT_ShouldUseTheBooleanMutator()
        {
            new Mutation().Create<BooleanMachine>().For("GreaterThan").Run().MutatorsUsed.Should().ContainSingle("BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsALT_ShouldUseTheBooleanMutator()
        {
            new Mutation().Create<BooleanMachine>().For("LessThan").Run().MutatorsUsed.Should().ContainSingle("BooleanMutator");
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsArithmeticAndBoolean_ShouldUseTheBooleanAndArithmeticMutator()
        {
            new Mutation().Create<Calculator>().For("DivWithCheck").Run().MutatorsUsed.Should().Contain(new List<string>{ "BooleanMutator", "ArithmeticMutator"});
        }
    }
}