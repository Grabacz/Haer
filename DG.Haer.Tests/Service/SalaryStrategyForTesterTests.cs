using DG.Haer.Service;
using NUnit.Framework;
using Assert = NUnit.Framework.Assert;

namespace DG.Haer.Tests.Unit.Service
{
    [TestFixture]
    public class SalaryStrategyForTesterTests
    {
        [Test]
        [TestCase(1, 2600)]
        [TestCase(2, 3575)]
        [TestCase(3, 3675)]
        [TestCase(4, 3775)]
        [TestCase(5, 4500)]
        public void CalculateCorrectSalary(byte experience, decimal expected)
        {
            var salaryStrategy = new SalaryStrategyForTester();

            var result = salaryStrategy.CalculateSalary(experience);

            Assert.AreEqual(result, expected);
        }
    }
}
