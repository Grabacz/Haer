using DG.Common;
using DG.Haer.Api.Infrastructure.Core;
using DG.Haer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DG.Haer.Api
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<Contact> Filter(this IEnumerable<Contact> @this, ContactsFilter filter)
        {
            return @this.ConditionalWhere(() => filter.Name != null, x => x.Name.Contains(filter.Name))
                .ConditionalWhere(() => filter.ContactType != null, x => x.ContactType == filter.ContactType)
                .ConditionalWhere(() => filter.Experience != null, x => x.Experience == filter.Experience)
                .ConditionalWhere(() => filter.Salary != null, x => x.Salary == filter.Salary)
                .ConditionalWhere(() => filter.SalaryGreaterThan5000 != false, x => x.Salary > 5000)
                .ConditionalWhere(() => filter.ExperiencedProgrammer != false, x => x.ContactType == ContactType.Programmer && x.Experience == 5);
        }
    }
}
