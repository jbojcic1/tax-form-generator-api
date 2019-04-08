using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dtos;

namespace TaxFormGeneratorApi.Services
{
    public interface IUserSettingsService
    {
        UserSettingsDto Get(int userId);
        void Upsert(int userId, UserSettingsDto settings);
    }

    public class UserSettingsService : IUserSettingsService
    {
        private readonly IRepository<UserSettings> userSettingsRepository;
        
        public UserSettingsService(IRepository<UserSettings> userSettingsRepository)
        {
            this.userSettingsRepository = userSettingsRepository;
        }

        public UserSettingsDto Get(int userId)
        {
            var userSettings = this.userSettingsRepository.GetOneOrNoneBy(x => x.UserId == userId);
            return userSettings?.ToDto();
        }

        public void Upsert(int userId, UserSettingsDto settings)
        {
            var newUserSettings = settings.ToDomain(userId);
            var userSettings = this.userSettingsRepository.GetOneOrNoneBy(x => x.UserId == userId);

            if (userSettings == null)
            {
                this.userSettingsRepository.Insert(newUserSettings);
            }
            else
            {
                newUserSettings.Id = userSettings.Id;
                this.userSettingsRepository.Update(newUserSettings);
            }
        }
    }
    
    public static class UserSettingsExtensions
    {
        // TODO: use auto mapper instead
        
        public static UserSettingsDto ToDto(this UserSettings userSettings)
        {
            return new UserSettingsDto
            {
                Personal = new PersonalSettingsDto
                {
                    PersonalOib = userSettings.Personal.Oib,
                    StreetName = userSettings.Personal.StreetName,
                    StreetNumber = userSettings.Personal.StreetNumber,
                    Postcode = userSettings.Personal.Postcode,
                    City = userSettings.Personal.City
                },
                City = new CitySettingsDto
                {
                    CityName = userSettings.Personal.City,
                    CityIban = userSettings.City.Iban,
                    CityCode = userSettings.City.Code,
                    Surtax = userSettings.City.Surtax
                },
                Company = new CompanySettingsDto
                {
                    CompanyOib = userSettings.Company.Oib,
                    CompanyEmail = userSettings.Company.Email,
                    CompanyName = userSettings.Company.Name,
                    CompanyStreet = userSettings.Company.Street,
                    CompanyCity = userSettings.Company.City
                },
                Salary = new SalarySettingsDto
                {
                    Amount = userSettings.Salary.Amount,
                    Currency = userSettings.Salary.Currency,
                    NonTaxableAmount = userSettings.Salary.NonTaxableAmount,
                    SalaryTax = userSettings.Salary.SalaryTax,
                    HealthInsuranceContribution = userSettings.Salary.HealthInsuranceContribution,
                    WorkSafetyContribution = userSettings.Salary.WorkSafetyContribution,
                    EmploymentContribution = userSettings.Salary.EmploymentContribution,
                    PensionPillar1Contribution = userSettings.Salary.PensionPillar1Contribution,
                    PensionPillar2Contribution = userSettings.Salary.PensionPillar2Contribution,
                },
                Dividend = new DividendSettingsDto
                {
                    DividendTax = userSettings.Dividend.DividendTax
                }
            };
        }
        
        public static UserSettings ToDomain(this UserSettingsDto userSettings, int userId)
        {
            return new UserSettings
            {
                UserId = userId,
                Personal = new PersonalSettings
                {
                    Oib = userSettings.Personal.PersonalOib,
                    StreetName = userSettings.Personal.StreetName,
                    StreetNumber = userSettings.Personal.StreetNumber,
                    Postcode = userSettings.Personal.Postcode,
                    City = userSettings.Personal.City
                },
                City = new CitySettings
                {
                    Iban = userSettings.City.CityIban,
                    Code = userSettings.City.CityCode,
                    Surtax = userSettings.City.Surtax
                },
                Company = new CompanySettings
                {
                    Oib = userSettings.Company.CompanyOib,
                    Email = userSettings.Company.CompanyEmail,
                    Name = userSettings.Company.CompanyName,
                    Street = userSettings.Company.CompanyStreet,
                    City = userSettings.Company.CompanyCity
                },
                Salary = new SalarySettings
                {
                    Amount = userSettings.Salary.Amount,
                    Currency = userSettings.Salary.Currency,
                    NonTaxableAmount = userSettings.Salary.NonTaxableAmount,
                    SalaryTax = userSettings.Salary.SalaryTax,
                    HealthInsuranceContribution = userSettings.Salary.HealthInsuranceContribution,
                    WorkSafetyContribution = userSettings.Salary.WorkSafetyContribution,
                    EmploymentContribution = userSettings.Salary.EmploymentContribution,
                    PensionPillar1Contribution = userSettings.Salary.PensionPillar1Contribution,
                    PensionPillar2Contribution = userSettings.Salary.PensionPillar2Contribution,
                },
                Dividend = new DividendSettings
                {
                    DividendTax = userSettings.Dividend.DividendTax
                }
            };
        }
    }
}