using DG.Haer.Domain;

namespace DG.Haer.Service.Services
{
    public class SalaryService : ISalaryService
    {
        private readonly ISalaryStrategyFactory _salaryStrategyFactory;

        public SalaryService(ISalaryStrategyFactory salaryStrategyFactory)
        {
            _salaryStrategyFactory = salaryStrategyFactory;
        }

        public decimal CalculateSalary(byte experience, ContactType contactType)
        {
            var salaryStrategy = _salaryStrategyFactory.CreateStrategy(contactType);
            var salary = salaryStrategy.CalculateSalary(experience);

            return salary;
        }
    }
}
