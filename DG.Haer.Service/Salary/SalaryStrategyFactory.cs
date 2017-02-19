using DG.Haer.Domain;
using System;

namespace DG.Haer.Service
{
    public class SalaryStrategyFactory : ISalaryStrategyFactory
    {
        public ISalaryStrategy CreateStrategy(ContactType contactType)
        {
            switch(contactType)
            {
                case ContactType.Programmer:
                    return new SalaryStrategyForProgrammer();
                case ContactType.Tester:
                    return new SalaryStrategyForTester();
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
