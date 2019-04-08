using System.Globalization;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;

namespace TaxFormGeneratorApi.Domain
{
    public class UserSettings : IEntity
    {
        public int Id { get; set; }
        
        public virtual PersonalSettings Personal { get; set; }
        
        public virtual CitySettings City { get; set; }
        
        public virtual CompanySettings Company { get; set; }
        
        public virtual SalarySettings Salary { get; set; }
        
        public virtual DividendSettings Dividend { get; set; }

        public int UserId { get; set; }
        
        public virtual User User { get; set; }
    }

    public class PersonalSettings
    {
        public string Oib { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
    }
    
    public class CitySettings
    {
        public string Iban { get; set; }
        public string Code { get; set; }
        public double Surtax { get; set; }
    }

    public class CompanySettings
    {
        public string Oib { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }

    public class SalarySettings
    {
        public decimal Amount { get; set; }
        public string Currency { get; set; }
        public decimal NonTaxableAmount { get; set; }
        public double SalaryTax { get; set; }
        public double HealthInsuranceContribution { get; set; }
        public double WorkSafetyContribution { get; set; }
        public double EmploymentContribution { get; set; }
        public double PensionPillar1Contribution { get; set; }
        public double PensionPillar2Contribution { get; set; }
    }

    public class DividendSettings
    {
        public double DividendTax { get; set; }
    }
}