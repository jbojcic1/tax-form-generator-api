using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Services;

namespace TaxFormGeneratorApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        [HttpPost]
        public ActionResult<AuthDto> Login(CredentialsDto model)
        {
            var response = this.authService.Authenticate(model.Email, model.Password);

            if (!response.Success) return Unauthorized(response.ErrorMessage);

            // TODO: use auto mapper
            return Ok(new AuthDto
            {
                AccessToken      = response.AccessToken,
                AccessExpiresIn  = response.AccessExpiresIn,
                RefreshToken     = response.RefreshToken,
                RefreshExpiresIn = response.RefreshExpiresIn
            });
        }
        
        [Route("refresh")]
        [HttpPost]
        public ActionResult<RefreshTokenResponseDto> RefreshToken(RefreshTokenDto model)
        {
            var response = this.authService.RefreshToken(model.RefreshToken);

            if (response.ErrorMessage != null) return Unauthorized(response.ErrorMessage);

            // TODO: use auto mapper
            return Ok(new RefreshTokenResponseDto
            {
                Token = response.Token,
                ExpiresIn = response.ExpiresIn
            });
        }
    }
}