using DG.Haer.Data;
using DG.Haer.Domain;
using DG.Haer.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DG.Haer.Tests.Service
{
    [TestFixture]
    public class ContactsServiceTests
    {
        List<Contact> _contacts;
        IContactsRepository _contactsRepository;
        IUnitOfWork _unitOfWork;
        IContactsService _contactsService;

        [SetUp]
        public void SetUp()
        {
            SetupContacts();
            SetUpContactsRepository();
            SetUpUnitOfWork();
            SetUpContactsService();
        }

        public void SetupContacts()
        {
            int counter = 0;
            _contacts = AppDbDataSeeder.GetContacts();
            _contacts.ForEach(contact => contact.Id = ++counter);
        }

        public void SetUpContactsRepository()
        {
            var repo = new Mock<IContactsRepository>();

            repo.Setup(x => x.GetAll()).Returns(_contacts);
            repo.Setup(x => x.GetById(It.IsAny<int>())).Returns(new Func<int, Contact>(id => _contacts.Find(x => x.Id == id)));
            repo.Setup(x => x.Add(It.IsAny<Contact>())).Callback(new Action<Contact>(newContact =>
            {
                var id = _contacts.Last().Id;
                id++;
                newContact.Id = id;
                _contacts.Add(newContact);
            }));
            repo.Setup(x => x.AddRange(It.IsAny<IEnumerable<Contact>>())).Callback(new Action<IEnumerable<Contact>>(newContacts =>
            {
                var lastId = _contacts.Last().Id;
                foreach (Contact contact in newContacts)
                {
                    contact.Id = lastId++;
                    _contacts.Add(contact);
                }
            }));
            repo.Setup(x => x.Exists(It.IsAny<int>())).Returns(new Func<int, bool>(id => _contacts.Exists(x => x.Id == id)));
            repo.Setup(x => x.Get(It.IsAny<Expression<Func<Contact, bool>>>())).Returns(new Func<Expression<Func<Contact, bool>>, IEnumerable<Contact>>(expression => _contacts.Where(expression.Compile())));
            repo.Setup(x => x.NotExists(It.IsAny<int>())).Returns(new Func<int, bool>(id => !_contacts.Exists(x => x.Id == id)));
            repo.Setup(x => x.Remove(It.IsAny<Contact>())).Callback(new Action<Contact>(contactToRemove =>
            {
                var findedContact = _contacts.Find(x => x.Id == contactToRemove.Id);
                if (findedContact != null)
                    _contacts.Remove(findedContact);
            }));
            repo.Setup(x => x.Update(It.IsAny<Contact>())).Callback(new Action<Contact>(inContact =>
            {
                var findedContact = _contacts.Find(x => x.Id == inContact.Id);
                findedContact.Name = inContact.Name;
                findedContact.ContactType = inContact.ContactType;
                findedContact.Experience = inContact.Experience;
                findedContact.Salary = inContact.Salary;
            }));

            _contactsRepository = repo.Object;
        }

        public void SetUpUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>().Object;
        }

        public void SetUpContactsService()
        {
            _contactsService = new ContactsService(_contactsRepository, _unitOfWork);
        }

        [Test]
        public void ServiceShouldGetAllContacts()
        {
            Assert.AreEqual(_contacts, _contactsService.GetContacts());
            Assert.AreEqual(_contacts.Count(), _contactsService.GetContacts().Count());
        }

        [Test]
        public void ServiceShouldAddNewContact()
        {
            var contact = new Contact()
            {
                Name = "Test1",
                ContactType = ContactType.Programmer,
                Experience = 1,
                Salary = 1
            };

            var maxId = _contacts.Max(x => x.Id);
            _contactsService.AddContact(contact);

            Assert.That(contact, Is.EqualTo(_contacts.Last()));
            Assert.That(maxId + 1, Is.EqualTo(_contacts.Last().Id));
        }
    }
}
