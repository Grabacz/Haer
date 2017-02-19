using System;
using System.Collections.Generic;
using System.Linq;

namespace DG.Haer.Service
{
    public class SalaryStrategyForTester : ISalaryStrategy
    {
        private Dictionary<Func<int, bool>, Action> _cases;
        private decimal _minSalary;

        public SalaryStrategyForTester()
        {
            _cases = new Dictionary<Func<int, bool>, Action>
            {
                { x => x < 1, () => _minSalary = 0m },
                { x => x < 2, () => _minSalary = 2000m },
                { x => x <= 4, () => _minSalary = 2700m },
                { x => x > 4, () => _minSalary = 3200m }
            };
        }

        public decimal CalculateSalary(byte experience)
        {
            _cases.First(x => x.Key(experience)).Value();

            if (_minSalary == 0m)
                throw new NotSupportedException();

            return _minSalary + experience * 100 + _minSalary / 4;
        }
    }
}
