using System;
using System.Collections.Generic;
using System.Linq;

namespace DG.Haer.Service
{
    public class SalaryStrategyForProgrammer : ISalaryStrategy
    {
        private Dictionary<Func<int, bool>, Action> _cases;
        private decimal _minSalary;

        public SalaryStrategyForProgrammer()
        {
            _cases = new Dictionary<Func<int, bool>, Action>
            {
                { x => x < 1, () => _minSalary = 0m },
                { x => x < 3, () => _minSalary = 2500m },
                { x => x <= 5, () => _minSalary = 5000m },
                { x => x > 5, () => _minSalary = 5500m }
            };
        }

        public decimal CalculateSalary(byte experience)
        {
            _cases.First(x => x.Key(experience)).Value();

            if (_minSalary == 0m)
                throw new NotSupportedException();

            return _minSalary + experience * 125;
        }
    }
}
