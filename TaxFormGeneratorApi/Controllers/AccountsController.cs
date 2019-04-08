using Microsoft.AspNetCore.Mvc;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Services;

namespace TaxFormGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;
        
        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        [HttpPost]
        public ActionResult Post([FromBody] RegistrationDto account)
        {
            // TODO: add validation

            this.accountService.Create(account);

            return Created("", null);
        }
    }
}