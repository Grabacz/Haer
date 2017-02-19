using DG.Haer.Api.Infrastructure.Core;
using DG.Haer.Domain;
using DG.Haer.Service;
using System.Web.Http;

namespace DG.Haer.Api.Controllers
{
    [RoutePrefix("api/salary")]
    public class SalaryController : WebApiController
    {
        private readonly ISalaryService _salaryService;

        public SalaryController(ISalaryService salaryService)
        {
            _salaryService = salaryService;
        }

        [HttpPut]
        [Route("calculate")]
        public IHttpActionResult CalculateSalary(ContactInfo contactInfo)
        {
            return Ok(_salaryService.CalculateSalary(contactInfo.Experience, contactInfo.ContactType));
        }
    }

    public class ContactInfo
    {
        public ContactType ContactType { get; set; }
        public byte Experience { get; set; }
    }

}
