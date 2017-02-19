using DG.Haer.Domain;

namespace DG.Haer.Service
{
    public interface ISalaryStrategyFactory
    {
        ISalaryStrategy CreateStrategy(ContactType contactType);
    }
}
