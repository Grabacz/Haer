using DG.Haer.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using System;
using Assert = NUnit.Framework.Assert;

namespace DG.Haer.Tests
{
    [TestFixture]
    public class SalaryStrategyForProgrammerTests
    {
        [TestCase(1, 2625)]
        [TestCase(2, 2750)]
        [TestCase(3, 5375)]
        [TestCase(4, 5500)]
        [TestCase(5, 5625)]
        [TestCase(6, 6250)]
        public void CalculateCorrectSalary(byte experience, decimal expected)
        {
            var salaryStrategy = new SalaryStrategyForProgrammer();

            var result = salaryStrategy.CalculateSalary(experience);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void WhenExperienceIsZeroThrowsException()
        {
            var salaryStrategy = new SalaryStrategyForProgrammer();
    
            Assert.Catch<NotSupportedException>(() => salaryStrategy.CalculateSalary(0));
        }
    }
}
