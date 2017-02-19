using DG.Haer.Domain;
using System.Collections.Generic;

namespace DG.Haer.Service
{
    public interface IContactsService
    {
        IEnumerable<Contact> GetContacts();
        void AddContact(Contact contact);
        void SaveContact();
    }
}
