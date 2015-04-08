using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using VGA.Mutations.TestAssembly;

namespace VGA.Mutations.Tests
{
    [TestFixture]
    public class BooleanMachineTests
    {
        [Test]
        public void TestGreaterThan()
        {
            var booleanMachine = new BooleanMachine();
            var result = booleanMachine.GreaterThan(2, 1);

            Assert.IsTrue(result);
        }

        [Test]
        public void TestLessThan()
        {
            var booleanMachine = new BooleanMachine();
            var result = booleanMachine.LessThan(2, 1);

            Assert.IsFalse(result);
        }
    }
}
