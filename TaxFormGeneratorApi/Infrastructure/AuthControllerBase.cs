using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TaxFormGeneratorApi.Infrastructure
{
    public class AuthControllerBase : ControllerBase
    {
        protected int LoggedInUserId => int.Parse(User.FindFirstValue("id")); // TODO: see how to better handle this with injectable service
    }
}