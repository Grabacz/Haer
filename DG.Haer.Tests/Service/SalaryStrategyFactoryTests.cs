using DG.Haer.Domain;
using DG.Haer.Service;
using NUnit.Framework;
using System;

namespace DG.Haer.Tests.Service
{
    [TestFixture]
    public class SalaryStrategyFactoryTests
    {
        ISalaryStrategyFactory _salaryStrategyFactory;

        [SetUp]
        public void SetUp()
        {
            _salaryStrategyFactory = new SalaryStrategyFactory();
        }

        [TestCase(ContactType.Programmer, typeof(SalaryStrategyForProgrammer))]
        [TestCase(ContactType.Tester, typeof(SalaryStrategyForTester))]
        public void ReturnCorrectStrategyInstance(ContactType contactType, Type expectedType)
        {
            Assert.IsInstanceOf(expectedType, _salaryStrategyFactory.CreateStrategy(contactType));
        }

        [Test]
        public void ThrowsNotSupportedExceptionForNotSupportedContactType()
        {
            Assert.Catch<NotSupportedException>(() => _salaryStrategyFactory.CreateStrategy(ContactType.NotSet));
        }
    }
}
