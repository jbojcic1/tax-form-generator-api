namespace TaxFormGeneratorApi.Dtos
{
    public class UserSettingsDto
    {
        public virtual PersonalSettingsDto Personal { get; set; }
        
        public virtual CitySettingsDto City { get; set; }
        
        public virtual CompanySettingsDto Company { get; set; }
        
        public virtual SalarySettingsDto Salary { get; set; }
        
        public virtual DividendSettingsDto Dividend { get; set; }
    }
    
    public class PersonalSettingsDto 
    {
        public string PersonalOib { get; set; }
        public string StreetName { get; set; }
        public string StreetNumber { get; set; }
        public string Postcode { get; set; }
        public string City { get; set; }
    }
    
    public class CitySettingsDto 
    {
        public string CityName { get; set; }
        public string CityIban { get; set; }
        public string CityCode { get; set; }
        public double Surtax { get; set; }
    }
    
    public class CompanySettingsDto 
    {
        public string CompanyOib { get; set; }
        public string CompanyEmail { get; set; }
        public string CompanyName { get; set; }
        public string CompanyStreet { get; set; }
        public string CompanyCity { get; set; }
    }
    
    public class SalarySettingsDto
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
    
    public class DividendSettingsDto
    {
        public double DividendTax { get; set; }
    }
}