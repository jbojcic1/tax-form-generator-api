using Microsoft.AspNetCore.Mvc;
using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dtos;

namespace TaxFormGeneratorApi.Services
{
    public interface IAccountService
    {
        void Create(RegistrationDto account);
    }

    public class AccountService : IAccountService
    {
        private readonly IRepository<User> userRepository;
        private readonly IPasswordHasher passwordHasher;
        
        public AccountService(IRepository<User> userRepository, IPasswordHasher passwordHasher)
        {
            this.userRepository = userRepository;
            this.passwordHasher = passwordHasher;
        }

        public void Create(RegistrationDto account)
        {
            var hashResult = this.passwordHasher.HashPassword(account.Password);
            
            // TODO: consider auto mapper
            var user = new User
            {
                Email = account.Email,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Password = hashResult.Password,
                Salt = hashResult.Salt
            };
            
            this.userRepository.Insert(user);
        }
    }
}