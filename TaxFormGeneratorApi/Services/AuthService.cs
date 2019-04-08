using System;
using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dtos;

namespace TaxFormGeneratorApi.Services
{
    public interface IAuthService
    {
        AuthModel Authenticate(string email, string password);
        RefreshModel RefreshToken(string refreshToken);
    }

    public class AuthService : IAuthService
    {
        private const int AccessTokenTtlInMinutes = 1;
        private const int RefreshTokenTtlInMinutes = 2; // Five days
        private const string AuthFailedMessage = "Invalid email and/or password.";
        
        private readonly IRepository<User> userRepository;
        private readonly ITokenProviderService tokenProvider;
        private readonly IPasswordHasher passwordHasher;

        public AuthService(
            IRepository<User> userRepository, 
            ITokenProviderService tokenProvider, 
            IPasswordHasher passwordHasher
        ) {
            this.userRepository = userRepository;
            this.tokenProvider = tokenProvider;
            this.passwordHasher = passwordHasher;
        }

        public AuthModel Authenticate(string email, string password)
        {
            var user = this.userRepository.GetOneOrNoneBy(u => u.Email == email);

            if (user == null)
            {
                return new AuthModel { Success = false, ErrorMessage = AuthFailedMessage };
            }

            var isValidPassword = this.passwordHasher.VerifyPassword(user.Password, user.Salt, password);

            if (!isValidPassword)
            {
                return new AuthModel { Success = false, ErrorMessage = AuthFailedMessage };
            }
            
            var accessTokenExpiry = DateTime.UtcNow.AddMinutes(AccessTokenTtlInMinutes);
            var refreshTokenExpiry = DateTime.UtcNow.AddMinutes(RefreshTokenTtlInMinutes);

            var accessToken = this.tokenProvider.CreateToken(user, accessTokenExpiry);
            var refreshToken = this.tokenProvider.CreateToken(user, refreshTokenExpiry);

            user.RefreshToken = refreshToken;
            this.userRepository.Update(user);

            return new AuthModel {
                Success          = true,
                AccessToken      = accessToken,
                AccessExpiresIn  = AccessTokenTtlInMinutes * 60,
                RefreshToken     = refreshToken,
                RefreshExpiresIn = RefreshTokenTtlInMinutes * 60,
            };
        }

        public RefreshModel RefreshToken(string refreshToken)
        {
            var user = this.userRepository.GetOneOrNoneBy(u => u.RefreshToken == refreshToken);

            if (user == null)
            {
                return new RefreshModel
                {
                    ErrorMessage = "Invalid token."
                };
            }

            var validationMetadata = this.tokenProvider.ValidateAndDecode(refreshToken);

            if (!validationMetadata.Valid)
            {
                return new RefreshModel
                {
                    ErrorMessage = validationMetadata.ValidationMessage
                };
            }

            var expiry = DateTime.UtcNow.AddMinutes(RefreshTokenTtlInMinutes);
            
            return new RefreshModel {
                Token     = this.tokenProvider.CreateToken(user, expiry),
                ExpiresIn = AccessTokenTtlInMinutes * 60
            };
        }
    }
    
    public class AuthModel
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public string AccessToken { get; set; }
        
        public int AccessExpiresIn { get; set; }
        
        public string RefreshToken { get; set; }
        
        public int RefreshExpiresIn { get; set; }
    }

    public class RefreshModel
    {
        public string Token { get; set; }
        
        public int ExpiresIn { get; set; }
        
        public string ErrorMessage { get; set; }
    }
}