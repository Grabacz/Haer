using DG.Haer.Domain;

namespace DG.Haer.Api.Infrastructure.Core
{
    public class ContactsFilter
    {
        public string Name { get; set; }
        public byte? Experience { get; set; }
        public decimal? Salary { get; set; }
        public ContactType? ContactType { get; set; }
        public bool ExperiencedProgrammer { get; set; }
        public bool SalaryGreaterThan5000 { get; set; }

    }
}
