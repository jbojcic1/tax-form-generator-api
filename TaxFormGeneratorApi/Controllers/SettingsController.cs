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
    public class SettingsController : AuthControllerBase
    {
        private readonly IUserSettingsService userSettingsService;

        public SettingsController(IUserSettingsService userSettingsService)
        {
            this.userSettingsService = userSettingsService;
        }

        [HttpGet]
        public UserSettingsDto Get()
        {
            return this.userSettingsService.Get(LoggedInUserId);
        }
        
        [HttpPost]
        public void Post(UserSettingsDto settings)
        {
            this.userSettingsService.Upsert(LoggedInUserId, settings);
        }
    }
}