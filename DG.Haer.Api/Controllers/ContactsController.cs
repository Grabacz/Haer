
using DG.Common;
using DG.Haer.Api.Core;
using DG.Haer.Api.Infrastructure.Core;
using DG.Haer.Business;
using DG.Haer.Domain;
using DG.Haer.Service;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DG.Haer.Api.Controllers
{
    [RoutePrefix("api/contacts")]
    public class ContactsController : WebApiController
    {
        private readonly int _pageSize = int.Parse(ConfigurationManager.AppSettings["pageSize"]);
        private readonly IContactsService _contactsService;

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpPut]
        [Route("list")]
        [ResponseType(typeof(PaginationSet<ContactViewModel>))]
        public IHttpActionResult Get(SearchSet searchSet)
        {
            var entities = _contactsService.GetContacts()
                .OrderBy(x => x.Id)
                .ConditionalWhere(() => searchSet.Filter.Name != null, x => x.Name.Contains(searchSet.Filter.Name))
                .ConditionalWhere(() => searchSet.Filter.ContactType != null, x => x.ContactType == searchSet.Filter.ContactType)
                .ConditionalWhere(() => searchSet.Filter.Experience != null, x => x.Experience == searchSet.Filter.Experience)
                .ConditionalWhere(() => searchSet.Filter.Salary != null, x => x.Salary == searchSet.Filter.Salary)
                .ConditionalWhere(() => searchSet.Filter.SalaryGreaterThan5000 != false, x => x.Salary > 5000)
                .ConditionalWhere(() => searchSet.Filter.ExperiencedProgrammer != false, x => x.ContactType == ContactType.Programmer && x.Experience == 5);

            IEnumerable<Contact> pageEntities = entities
                .Skip((searchSet.SelectedPage - 1) * _pageSize)
                .Take(_pageSize);

            var vms = pageEntities.ToBussinessObjects<ContactViewModel>();

            var response = new PaginationSet<ContactViewModel>()
            {
                CurrentPage = searchSet.SelectedPage,
                Items = vms,
                TotalItems = entities.Count(),
                ItemsPerPage = _pageSize
            };

            return Ok(response);
        }

        [HttpPost]
        [Route("add")]
        public IHttpActionResult Add(ContactViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                var response = ModelState.Keys.SelectMany(k => ModelState[k].Errors).Select(m => m.ErrorMessage).ToArray();
                return BadRequest(ModelState);
            }

            var entity = vm.ToDomainObject<Contact>();
            _contactsService.AddContact(entity);
            _contactsService.SaveContact();

            return Created("", entity);
        }
    }
}
