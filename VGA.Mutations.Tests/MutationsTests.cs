namespace VGA.Mutations.Tests
{
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
            new Mutation().Create<Calculator>().For("Add").MethodInfo.Should().NotBeNull();
        }
    }
}