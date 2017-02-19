using DG.Haer.Domain;

namespace DG.Haer.Service
{
    public interface ISalaryService
    {
        decimal CalculateSalary(byte experience, ContactType contactType);
    }
}
