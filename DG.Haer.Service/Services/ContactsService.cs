using DG.Haer.Data;
using DG.Haer.Domain;
using System.Collections.Generic;

namespace DG.Haer.Service
{
    public class ContactsService : IContactsService
    {
        private readonly IContactsRepository _contactsRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ContactsService(
            IContactsRepository contactsRepository,
            IUnitOfWork unitOfWork)
        {
            _contactsRepository = contactsRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Contact> GetContacts()
        {
            var contacts = _contactsRepository.GetAll();
            return contacts;
        }

        public void AddContact(Contact contact)
        {
            _contactsRepository.Add(contact);
        }

        public void SaveContact()
        {
            _unitOfWork.Commit();
        }
    }
}
