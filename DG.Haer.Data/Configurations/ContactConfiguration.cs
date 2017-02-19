using DG.Haer.Domain;

namespace DG.Haer.Data
{
    public class ContactConfiguration : EntityConfiguration<Contact>
    {
        public ContactConfiguration()
        {
            ToTable("Contacts");

            Property(x => x.Name).IsRequired().HasMaxLength(100);
            Property(x => x.Experience).IsRequired();
            Property(x => x.Salary).IsRequired();
        }
    }
}
