namespace VGA.Mutations.Tests
{
    using System.IO;
    using FakeItEasy;
    using FluentAssertions;
    using Moq;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class MutationsTests
    {
        private readonly ITestRunner _testRunner = A.Dummy<ITestRunner>();

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
        public void WhenTheMutationRuns_ShouldSaveTheOriginalFile()
        {
            Mutation.For<Calculator>().InMethod("Sub").WithTestRunner(A.Dummy<ITestRunner>()).Run();

            File.Exists(".\\VGA.Mutations.TestAssembly_original.dll").Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_ShouldRunTheTests()
        {
            var testRunner = new Mock<ITestRunner>();
            //Mutation.For<Calculator>().InMethod("Add").Run(testRunner.Object);

        }
    }
}