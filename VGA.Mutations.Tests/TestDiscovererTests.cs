namespace VGA.Mutations.Tests
{
    using System.Collections.Generic;
    using FluentAssertions;
    using Mutations.Mutators;
    using NUnit.Framework;

    [TestFixture]
    public class TestDiscovererTests
    {
        [Test]
        public void ShouldDiscoverTwoTestsThatCallAdd()
        {
            var testDiscoverer = new TestDiscoverer();
            var expected = new List<TestToExecute>
            {
                new TestToExecute(".\\VGA.Mutations.Tests.dll", "VGA.Mutations.Tests.CalculatorTests", "TestAdd"),
                new TestToExecute(".\\VGA.Mutations.Tests.dll", "VGA.Mutations.Tests.CalculatorTests", "TestCorrectAdd")
            };

            var testsToExecute = testDiscoverer.DiscoverTestsToExecute(".\\VGA.Mutations.TestAssembly.dll",
                "VGA.Mutations.TestAssembly.Calculator", "Add");

            testsToExecute.ShouldBeEquivalentTo(expected);
        }

        [Test]
        public void ShouldNotDiscoverAnyTestThatCallSub()
        {
            var testDiscoverer = new TestDiscoverer();

            var testsToExecute = testDiscoverer.DiscoverTestsToExecute(".\\VGA.Mutations.TestAssembly.dll",
                "VGA.Mutations.TestAssembly.Calculator", "Sub");

            testsToExecute.Should().HaveCount(0);
        }
    }
}