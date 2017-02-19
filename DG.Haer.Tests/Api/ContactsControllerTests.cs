using DG.Haer.Api.Controllers;
using DG.Haer.Business;
using DG.Haer.Data;
using DG.Haer.Domain;
using DG.Haer.Service;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Http.Results;
using DG.Haer.Api;
using System.Collections;
using DG.Haer.Api.Core;
using DG.Haer.Api.Infrastructure.Core;
using System.Net.Http;

namespace DG.Haer.Tests.Api
{
    [TestFixture]
    public class ContactsControllerTests
    {
        List<Contact> _contacts;
        IContactsRepository _contactsRepository;
        IUnitOfWork _unitOfWork;
        IContactsService _contactsService;

        ContactsController _controller;

        [SetUp]
        public void SetUp()
        {
            SetupContacts();
            SetUpContactsRepository();
            SetUpUnitOfWork();
            SetUpContactsService();
            SetUpContactsController();
        }

        public void SetupContacts()
        {
            int counter = 0;
            _contacts = AppDbDataSeeder.GetContacts();
            _contacts.ForEach(contact => contact.Id = ++counter);

            AutoMapperConfig.Configure();
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

        public void SetUpContactsController()
        {
            _controller = new ContactsController(_contactsService);
            _controller.PageSize = 4;
        }

        [Test]
        public void ControllerShouldReturnAllContacts()
        {
            var vms = (_controller.GetAll() as OkNegotiatedContentResult<IEnumerable<ContactViewModel>>).Content;

            var responseEntities = vms.ToDomainObjects<Contact>();

            CollectionAssert.AreEqual(_contacts, responseEntities, new ContactsComparer());
        }

        [Test]
        public void ControllerShouldSearchCorrect_EmptySearchSet()
        {
            var paginationSet = (_controller.Search(new SearchSet()) as OkNegotiatedContentResult<PaginationSet<ContactViewModel>>).Content;

            var entities = paginationSet.Items.ToDomainObjects<Contact>();
            var expected = _contacts.OrderBy(x => x.Id).Take(4);

            Assert.AreEqual(paginationSet.CurrentPage, 1);
            Assert.AreEqual(paginationSet.TotalPages, 3);
            Assert.AreEqual(paginationSet.TotalItems, 12);
            Assert.AreEqual(paginationSet.ItemsPerPage, 4);

            CollectionAssert.AreEqual(expected, entities, new ContactsComparer());
        }

        [Test]
        public void ControllerShouldSearchCorrect_NotEmptySearchSet_FirstPage()
        {
            var searchSet = new SearchSet()
            {
                SelectedPage = 1,
                Filter = new ContactsFilter()
                {
                    Name = "Kam"
                }
            };

            var paginationSet = (_controller.Search(searchSet) as OkNegotiatedContentResult<PaginationSet<ContactViewModel>>).Content;

            var entities = paginationSet.Items.ToDomainObjects<Contact>();

            var expected = 
                _contacts.OrderBy(x => x.Id)
                .Filter(searchSet.Filter)
                .Skip((searchSet.SelectedPage - 1) * _controller.PageSize)
                .Take(_controller.PageSize);

            Assert.AreEqual(paginationSet.CurrentPage, 1);
            Assert.AreEqual(paginationSet.TotalPages, 1);
            Assert.AreEqual(paginationSet.TotalItems, 2);
            Assert.AreEqual(paginationSet.ItemsPerPage, 4);

            CollectionAssert.AreEqual(expected, entities, new ContactsComparer());
        }

        [Test]
        public void ControllerShouldSearchCorrect_NotEmptySearchSet_Page2()
        {
            var searchSet = new SearchSet()
            {
                SelectedPage = 2,
                Filter = new ContactsFilter()
                {
                    Name = "K"
                }
            };

            var paginationSet = (_controller.Search(searchSet) as OkNegotiatedContentResult<PaginationSet<ContactViewModel>>).Content;

            var entities = paginationSet.Items.ToDomainObjects<Contact>();

            var expected =
                _contacts.OrderBy(x => x.Id)
                .Filter(searchSet.Filter)
                .Skip((searchSet.SelectedPage - 1) * _controller.PageSize)
                .Take(_controller.PageSize);

            Assert.AreEqual(paginationSet.CurrentPage, 2);
            Assert.AreEqual(paginationSet.TotalPages, 2);
            Assert.AreEqual(paginationSet.TotalItems, 5);
            Assert.AreEqual(paginationSet.ItemsPerPage, 4);

            CollectionAssert.AreEqual(expected, entities, new ContactsComparer());
        }

        [Test]
        public void ControllerSchouldAddContact_CorrectContact()
        {
            var vm = new ContactViewModel()
            {
                Id = 0,
                Name = "Test",
                Salary = 2600,
                ContactType = (byte)ContactType.Programmer,
                Experience = 5
            };

            _controller.Configuration = new System.Web.Http.HttpConfiguration();
            _controller.Request = new System.Net.Http.HttpRequestMessage()
            {
                Method = HttpMethod.Post
            };

            var response = _controller.Add(vm);

            Assert.IsInstanceOf<CreatedNegotiatedContentResult<ContactViewModel>>(response);
            Assert.AreEqual(13, _contacts.Count());
        }

        [Test]
        public void ControllerSchouldAddContact_ContactWithError()
        {
            // To short contact name
            var vm = new ContactViewModel()
            {
                Id = 0,
                Name = "a",
                Salary = 2600,
                ContactType = (byte)ContactType.Programmer,
                Experience = 5
            };

            _controller.Configuration = new System.Web.Http.HttpConfiguration();
            _controller.Request = new System.Net.Http.HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://localhost:8020/api/contacts/add")
            };

            var response = _controller.Add(vm);

            Assert.IsInstanceOf<BadRequestResult>(response);
            Assert.AreEqual(12, _contacts.Count());
        }
    }
}
