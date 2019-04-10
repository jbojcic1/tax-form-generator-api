using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Infrastructure;
using TaxFormGeneratorApi.Services;

namespace TaxFormGeneratorApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class FormsController : AuthControllerBase
    {
        private readonly IFormService formService;
        
        public FormsController(IFormService formService)
        {
            this.formService = formService;
        }

        [HttpGet]
        public IEnumerable<FormResponseDto> Get()
        {
            return this.formService.GetAll(LoggedInUserId);
        }
        
        // TODO: see how to handle this better instead of having controller action per form type
        
        [HttpPost]
        [Route("SALARY_JOPPD")]
        public void SaveSalaryJOPPD(SalaryJOPPDDto form)
        {
            this.formService.SaveForm(LoggedInUserId, form);
        }
        
        [HttpPost]
        [Route("DIVIDEND_JOPPD")]
        public void SaveDividendJOPPD(DividendJOPPDDto form)
        {
            this.formService.SaveForm(LoggedInUserId, form);
        }
    }
}