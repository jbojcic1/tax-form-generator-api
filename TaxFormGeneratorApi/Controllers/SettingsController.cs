using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Services;

namespace TaxFormGeneratorApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IUserSettingsService userSettingsService;

        public SettingsController(IUserSettingsService userSettingsService)
        {
            this.userSettingsService = userSettingsService;
        }

        [HttpGet]
        public UserSettingsDto Get()
        {
            var userId = int.Parse(User.Identity.Name); // TODO: see how to handle this better
            return this.userSettingsService.Get(userId);
        }
        
        [HttpPost]
        public void Post(UserSettingsDto settings)
        {
            var userId = int.Parse(User.Identity.Name);
            this.userSettingsService.Upsert(userId, settings);
        }
    }
}