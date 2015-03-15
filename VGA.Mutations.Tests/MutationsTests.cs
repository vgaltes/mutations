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
        public void WhenAMutationIsCreatedForAClass_AMutationClassObjectIsCreated()
        {
            Mutation.For<Calculator>().Should().BeOfType<MutationClass>();
        }


        [Test]
        public void WhenAMethodIsSpecified_AMutationRunnerObjectIsCreated()
        {
            Mutation.For<Calculator>().InMethod("Add").Should().BeOfType<MutationRunner>();
        }
        
        [Test]
        public void WhenTheMutationRuns_ShouldCreateANewFile()
        {
            Mutation.For<Calculator>().InMethod("Add").Run();

            File.Exists(".\\VGA.Mutations.TestAssembly_Mutated.dll").Should().BeTrue();
        }
    }
}