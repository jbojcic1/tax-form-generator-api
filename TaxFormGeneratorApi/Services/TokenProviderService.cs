using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Services
{
    public interface ITokenProviderService
    {
        string CreateToken(User user, DateTime expiry);

        TokenValidationParameters GetValidationParameters();

        TokenValidationMetadata ValidateAndDecode(string jwt);
    }

    public class TokenProviderService : ITokenProviderService
    {
        private readonly SymmetricSecurityKey key; 
        private readonly SigningCredentials credentials;
        private readonly string issuer;
        private readonly string audience;
        private readonly TokenValidationParameters validationParmeters;
        private readonly JwtSecurityTokenHandler tokenHandler;

        public TokenProviderService(string key, string issuer, string audience)
        {
            this.issuer = issuer;
            this.audience = audience;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            this.credentials = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha256);
            this.validationParmeters = new TokenValidationParameters
            {
                IssuerSigningKey = this.key,
                ValidAudience = this.audience,
                ValidIssuer = this.issuer,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromSeconds(0) // Identity and resource servers are the same.
            };
            this.tokenHandler = new JwtSecurityTokenHandler();
        }

        public string CreateToken(User user, DateTime expiry)
        {
            var identity = new ClaimsIdentity(new GenericIdentity(user.Email, "jwt"));

            var token = new JwtSecurityToken(
                this.issuer, 
                this.audience,
                identity.Claims,
                expires: expiry.ToUniversalTime(),
                signingCredentials: this.credentials
            );

            return this.tokenHandler.WriteToken(token);
        }

        public TokenValidationParameters GetValidationParameters()
        {
            return this.validationParmeters;
        }
        
        public TokenValidationMetadata ValidateAndDecode(string jwt)
        {
            ClaimsPrincipal claimsPrincipal = null;
            SecurityToken token = null;
            string message = null;
            
            try
            {
                claimsPrincipal = this.tokenHandler.ValidateToken(jwt, this.validationParmeters, out token);
            }
            catch (SecurityTokenExpiredException exception)
            {
                message = "Token expired.";
            }
            catch (SecurityTokenValidationException stvex)
            {
                // The token failed validation!
                message = "Invalid token.";
            }
            catch (ArgumentException argex)
            {
                // The token was not well-formed or was invalid for some other reason.
                message = "Invalid token.";
            }
            
            return new TokenValidationMetadata
            {
                Valid = claimsPrincipal != null,
                Token = (JwtSecurityToken) token,
                ValidationMessage = message
            };
        }
    }

    public class TokenValidationMetadata
    {
        public bool Valid { get; set; }

        public JwtSecurityToken Token { get; set; }

        public string ValidationMessage { get; set; }
    }
}