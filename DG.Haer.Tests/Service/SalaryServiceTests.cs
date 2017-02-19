using DG.Haer.Domain;
using DG.Haer.Service;
using DG.Haer.Service.Services;
using Moq;
using NUnit.Framework;
using System;

namespace DG.Haer.Tests.Service
{
    [TestFixture]
    public class SalaryServiceTests
    {
        ISalaryStrategyFactory _salaryStrategyFactory;
        ISalaryService _salaryService;

        [SetUp]
        public void SetUp()
        {
            SetuUpSalaryStrategyFactory();
            SetUpSalaryService();
        }
        private void SetUpSalaryService()
        {
            _salaryService = new SalaryService(_salaryStrategyFactory);
            
        }
  
        private void SetuUpSalaryStrategyFactory()
        {
            var salaryStrategy = new Mock<ISalaryStrategy>();
            salaryStrategy.Setup(x => x.CalculateSalary(It.IsAny<byte>())).Returns(new Func<byte, decimal>(x => x));

            var salaryServiceFactory = new Mock<ISalaryStrategyFactory>();
            salaryServiceFactory.Setup(x => x.CreateStrategy(It.IsAny<ContactType>())).Returns(new Func<ContactType, ISalaryStrategy>(x => salaryStrategy.Object));

            _salaryStrategyFactory = salaryServiceFactory.Object;
        }

        [TestCase(1, ContactType.Programmer, 1)]
        [TestCase(1, ContactType.Tester, 1)]
        [TestCase(5, ContactType.Programmer, 5)]
        [TestCase(5, ContactType.Tester, 5)]
        public void ServiceCalculateCorrectSalary(byte experience, ContactType contactType, decimal expected)
        {
            var result = _salaryService.CalculateSalary(experience, contactType);
            Assert.AreEqual(expected, result);
        }
    }
}
