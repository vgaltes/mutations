﻿namespace VGA.Mutations.Tests.Mutators
{
    using System.Linq;
    using FakeItEasy;
    using FluentAssertions;
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class ArithmeticMutatorTests
    {
        private readonly ITestRunner _testRunner = A.Dummy<ITestRunner>();

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsADiv_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Div")
                .WithTestRunner(_testRunner)
                .Run()
                .Any(m => m.MutatorUsed == "ArithmeticMutator")
                .Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAMult_ShouldReturnNoResults_BecauseThereIsNoTest()
        {
            Mutation.For<Calculator>()
                .InMethod("Mult")
                .WithTestRunner(_testRunner)
                .Run()
                .Should().HaveCount(0);
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsASub_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Sub")
                .WithTestRunner(_testRunner)
                .Run()
                .Any(m => m.MutatorUsed == "ArithmeticMutator")
                .Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Add")
                .WithTestRunner(_testRunner)
                .Run()
                .Any(m => m.MutatorUsed == "ArithmeticMutator")
                .Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnRem_ShouldUseTheArithmeticMutator()
        {
            Mutation.For<Calculator>()
                .InMethod("Rem")
                .WithTestRunner(_testRunner)
                .Run()
                .Any(m => m.MutatorUsed == "ArithmeticMutator")
                .Should().BeTrue();
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsArithmeticAndBoolean_ShouldUseTheBooleanAndArithmeticMutator()
        {
            var mutationResults = Mutation.For<Calculator>()
                .InMethod("DivWithCheck")
                .WithTestRunner(_testRunner)
                .Run();

            mutationResults.Count(mr => mr.MutatorUsed == "ArithmeticMutator").Should().Be(8);
            mutationResults.Count(mr => mr.MutatorUsed == "BooleanMutator").Should().Be(1);
        }

        [Test]
        public void WhenTheMutationRuns_IfTheCurrentOperationIsAnAdd_ShouldMutateWithSubMultDivAndRem()
        {
            var mutationResults = Mutation.For<Calculator>()
                .InMethod("Add")
                .WithTestRunner(_testRunner)
                .Run();

            mutationResults.Any(mr => mr.MutationPerformed.Contains("sub")).Should().BeTrue();
            mutationResults.Any(mr => mr.MutationPerformed.Contains("mul")).Should().BeTrue();
            mutationResults.Any(mr => mr.MutationPerformed.Contains("div")).Should().BeTrue();
            mutationResults.Any(mr => mr.MutationPerformed.Contains("rem")).Should().BeTrue();
        }

        [Test]
        [ExpectedException(typeof(MutationTestFailedException))]
        public void WhenMutatingAdd_ShouldSurviveOneTest()
        {
            Mutation.For<Calculator>()
                .InMethod("Add")
                .Run();
        }

        [Test]
        public void AFunctionWithoutATestShouldNotReturnAnyResult()
        {
            var mutationResults = Mutation.For<Calculator>()
                .InMethod("NoTestFunction")
                .WithTestRunner(_testRunner)
                .Run();

            mutationResults.Should().HaveCount(0);
        }
    }
}