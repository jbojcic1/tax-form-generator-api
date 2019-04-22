using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dtos;
using TaxFormGeneratorApi.Services;
using Tests.Helpers;
using Tests.Infrastructure;
using Xunit;

namespace Tests.Integration
{
    public class UserSettingsServiceTests : IClassFixture<CustomWebApplicationFactory<TaxFormGeneratorApi.Startup>>, IDisposable
    {
        private readonly IUserSettingsService _service;
        private readonly CustomWebApplicationFactory<TaxFormGeneratorApi.Startup> _factory;
        private readonly IServiceScope _scope;
        private readonly IServiceProvider _serviceProvider;
        private readonly IRepository<UserSettings> _userSettingsRepository;
        private readonly UserSettingsDto _newUserSettingsDto;

        public UserSettingsServiceTests(CustomWebApplicationFactory<TaxFormGeneratorApi.Startup> factory) {
            _factory = factory;
            _factory.CreateClient();
            
            _scope = _factory.Server.Host.Services.CreateScope();
            _serviceProvider = _scope.ServiceProvider;
            
            _service = _serviceProvider.GetRequiredService<IUserSettingsService>();
            _userSettingsRepository = _serviceProvider.GetRequiredService<IRepository<UserSettings>>();
            
            _newUserSettingsDto = new UserSettingsDto
            {
                City = new CitySettingsDto
                {
                    CityName = "Split",
                    CityCode = "fdsfs",
                    CityIban = "HR128888024894203",
                    Surtax = 0.05
                },
                Company = new CompanySettingsDto
                {
                    CompanyCity = "Split",
                    CompanyEmail = "aa@bb.com",
                    CompanyName = "Best company",
                    CompanyOib = "654576464",
                    CompanyStreet = "Mazuraniceva 12"
                },
                Dividend = new DividendSettingsDto
                {
                    DividendTax = 0.12
                },
                Personal = new PersonalSettingsDto
                {
                    City = "Split",
                    PersonalOib = "5645454343",
                    Postcode = "21000",
                    StreetName = "Sukoisanska",
                    StreetNumber = "2"
                },
                Salary = new SalarySettingsDto
                {
                    Amount = 1000,
                    Currency = "EUR",
                    EmploymentContribution = 0,
                    HealthInsuranceContribution = 0,
                    NonTaxableAmount = 3800,
                    PensionPillar1Contribution = 0,
                    PensionPillar2Contribution = 0,
                    SalaryTax = 0,
                    WorkSafetyContribution = 0.02
                }
            };
        }

        [Fact]
        public void Get_WhenCalledWithExistingUserId_ShouldReturnUserSettings()
        {
            var users = Utilities.GetSeedingUsers();
            var bob = users.Find(x => x.Id == 1);
            var userSettings = Utilities.GetSeedingUserSettings(users).Find(x => x.User == bob);
            var userSettingsDto = userSettings.ToDto();
            
            var result = _service.Get(1);

            result.Should().BeEquivalentTo(userSettingsDto);
        }

        [Fact]
        public void Upsert_ShouldAddNewSettingsForTheUserIfTheyDontExist()
        {
            var userId = 2;

            var userSettings = _userSettingsRepository.GetOneOrNoneBy(x => x.UserId == userId);

            userSettings.Should().BeNull();

            _service.Upsert(userId, _newUserSettingsDto);
            
            userSettings = _userSettingsRepository.GetOneOrNoneBy(x => x.UserId == userId);

            userSettings.Should().NotBeNull();
        }

        [Fact]
        public void Upsert_ShouldUpdateExistingUserSettingsWhenUserHasThem()
        {
            var userId = 1;

            var userSettings = _userSettingsRepository.GetOneOrNoneBy(x => x.UserId == userId);
            
            userSettings.Should().NotBeNull();
            
            _service.Upsert(userId, _newUserSettingsDto);
            
            var userSettingsDto = _userSettingsRepository.GetOneOrNoneBy(x => x.UserId == userId).ToDto();

            userSettingsDto.Should().BeEquivalentTo(_newUserSettingsDto);
        }

        public void Dispose()
        {
            _scope?.Dispose();
        }
    }
}