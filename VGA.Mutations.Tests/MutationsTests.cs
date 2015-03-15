namespace VGA.Mutations.Tests
{
    using System.IO;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class MutationsTests
    {
        [Test]
        public void WhenAMethodIsSpecified_TheMethodInfoShouldBeLoaded()
        {
            new Mutation().Create<Calculator>().For("Add").MethodDefinition.Should().NotBeNull();
        }

        [Test]
        public void WhenAMutationIsCreated_TheAssemblyShouldBeLoaded()
        {
            new Mutation().Create<Calculator>().Assembly.Should().NotBeNull();
        }

        [Test]
        public void WhenTheMutationRuns_ShouldCreateANewFile()
        {
            new Mutation().Create<Calculator>().For("Add").Run();

            File.Exists(".\\VGA.Mutations.TestAssembly_Mutated.dll").Should().BeTrue();
        }
    }
}