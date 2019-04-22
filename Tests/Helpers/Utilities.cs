using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Services;

namespace Tests.Helpers
{
    public static class Utilities
    {
        public static void InitializeDbForTests(TaxFormGeneratorContext db)
        {
            var users = GetSeedingUsers();
            db.Users.AddRange(GetSeedingUsers());
            db.UserSettings.AddRange(GetSeedingUserSettings(users));
            db.SaveChanges();
        }

        public static List<User> GetSeedingUsers()
        {
            var passwordHasher = new PasswordHasher();

            var hashResultBob = passwordHasher.HashPassword("123456");
            var hashResultTom = passwordHasher.HashPassword("asdfgh");
            
            return new List<User>()
            {
                new User
                {
                    Id = 1,
                    FirstName = "Bob",
                    LastName = "Bobovski",
                    Email = "bob@bobovski.com",
                    Password = hashResultBob.Password,
                    Salt = hashResultBob.Salt
                },
                new User
                {
                    Id = 2,
                    FirstName = "Tom",
                    LastName = "Sawyer",
                    Email = "tom@sawyer.com",
                    Password = hashResultTom.Password,
                    Salt = hashResultTom.Salt
                }
            };
        }
        
        public static List<UserSettings> GetSeedingUserSettings(List<User> users)
        {
            var bob = users.Find(x => x.Email == "bob@bobovski.com");
            
            return new List<UserSettings>()
            {
                new UserSettings
                {
                    User = bob,
                    City = new CitySettings
                    {
                        Code = "12345",
                        Iban = "HR123009024894203",
                        Surtax = 0.05
                    },
                    Company = new CompanySettings
                    {
                        City = "New York",
                        Email = "fds@gfds.com",
                        Name = "Kramerica Industries LLC",
                        Oib = "1234567890",
                        Street = "Fifth Avenue 34"
                    },
                    Dividend = new DividendSettings
                    {
                        DividendTax = 0.12
                    },
                    Personal = new PersonalSettings
                    {
                        City = "Ruzic",
                        Oib = "548379578434",
                        Postcode = "22322",
                        StreetName = "Bojcici",
                        StreetNumber = "16"
                    },
                    Salary = new SalarySettings
                    {
                        Amount = 500,
                        Currency = "EUR",
                        EmploymentContribution = 0,
                        HealthInsuranceContribution = 0,
                        NonTaxableAmount = 3800,
                        PensionPillar1Contribution = 0.2,
                        PensionPillar2Contribution = 0.1,
                        SalaryTax = 0.2,
                        WorkSafetyContribution = 0
                    }
                }
            };
        }
    }
}