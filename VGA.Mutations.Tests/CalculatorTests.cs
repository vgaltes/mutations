namespace VGA.Mutations.Tests
{
    using NUnit.Framework;
    using TestAssembly;

    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestAdd()
        {
            var calculator = new Calculator();
            var result = calculator.Add(2, 2);

            Assert.AreEqual(4, result);
        }

        [Test]
        public void TestCorrectAdd()
        {
            var calculator = new Calculator();
            var result = calculator.Add(2, 3);

            Assert.AreEqual(5, result);
        }

        [Test]
        public void TestSub()
        {
            var calculator = new Calculator();
            var result = calculator.Sub(2, 1);

            Assert.AreEqual(1, result);
        }
    }
}