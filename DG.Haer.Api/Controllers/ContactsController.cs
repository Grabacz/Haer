
using DG.Common;
using DG.Haer.Api.Core;
using DG.Haer.Api.Infrastructure.Core;
using DG.Haer.Business;
using DG.Haer.Domain;
using DG.Haer.Service;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;

namespace DG.Haer.Api.Controllers
{
    [RoutePrefix("api/contacts")]
    public class ContactsController : WebApiController
    {
        private readonly IContactsService _contactsService;

        public int PageSize { get; set; } = int.Parse(ConfigurationManager.AppSettings["pageSize"]);

        public ContactsController(IContactsService contactsService)
        {
            _contactsService = contactsService;
        }

        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(IEnumerable<ContactViewModel>))]
        public IHttpActionResult GetAll()
        {
            var entities = _contactsService.GetContacts();
            var vms = entities.ToBussinessObjects<ContactViewModel>();

            return Ok(vms);
        }

        [HttpPut]
        [Route("search")]
        [ResponseType(typeof(PaginationSet<ContactViewModel>))]
        public IHttpActionResult Search(SearchSet searchSet)
        {
            int itemsCount;

            var entities = _contactsService.GetContacts()
                .OrderBy(x => x.Id)
                .Filter(searchSet.Filter)
                .GetCount(out itemsCount)
                .Skip((searchSet.SelectedPage - 1) * PageSize)
                .Take(PageSize);

            var vms = entities.ToBussinessObjects<ContactViewModel>();

            var response = new PaginationSet<ContactViewModel>()
            {
                CurrentPage = searchSet.SelectedPage,
                Items = vms,
                TotalItems = itemsCount,
                ItemsPerPage = PageSize
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

            var responseEntity = entity.ToBussinessObject<ContactViewModel>();

            return Created("", responseEntity);
        }
    }
}
