using DG.Haer.Domain;

namespace DG.Haer.Data
{
    public class ContactsRepository : Repository<Contact>, IContactsRepository
    {
        public ContactsRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {

        }
    }
}
