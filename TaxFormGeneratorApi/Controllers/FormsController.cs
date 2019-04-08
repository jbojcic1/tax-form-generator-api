using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Services;

namespace TaxFormGeneratorApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : ControllerBase
    {
        private readonly IFormService formService;
        
        public FormsController(IFormService formService)
        {
            this.formService = formService;
        }

        [HttpGet]
        public IEnumerable<FormResponseDto> Get()
        {
            var userId = int.Parse(User.Identity.Name);
            return this.formService.GetAll(userId);
        }
        
        // TODO: see how to handle this better instead of having controller action per form type
        
        [HttpPost]
        [Route("SALARY_JOPPD")]
        public void SaveSalaryJOPPD(SalaryJOPPDDto form)
        {
            var userId = int.Parse(User.Identity.Name);
            this.formService.SaveForm(userId, form);
        }
        
        [HttpPost]
        [Route("DIVIDEND_JOPPD")]
        public void SaveDividendJOPPD(DividendJOPPDDto form)
        {
            var userId = int.Parse(User.Identity.Name);
            this.formService.SaveForm(userId, form);
        }
    }
}