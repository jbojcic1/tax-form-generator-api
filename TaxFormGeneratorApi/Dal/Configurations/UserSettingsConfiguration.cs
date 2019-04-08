using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Dal.Configurations
{    
    public class UserSettingsConfiguration: IEntityTypeConfiguration<UserSettings>
    {
        public void Configure(EntityTypeBuilder<UserSettings> builder)
        {
            builder.ToTable("UserSettings");

            builder.OwnsOne(
                o => o.Personal,
                personal =>
                {
                    personal.Property(x => x.Oib).HasColumnName("PersonalOib").IsRequired();
                    personal.Property(x => x.City).HasColumnName("City").IsRequired();
                    personal.Property(x => x.Postcode).HasColumnName("Postcode").IsRequired();
                    personal.Property(x => x.StreetName).HasColumnName("StreetName").IsRequired();
                    personal.Property(x => x.StreetNumber).HasColumnName("StreetNumber").IsRequired();

                    // ReSharper disable once HeapView.BoxingAllocation
                    personal.HasIndex(x => x.Oib).IsUnique();
                }
            );
            
            builder.OwnsOne(
                o => o.City,
                city =>
                {
                    city.Property(x => x.Iban).HasColumnName("CityIban").IsRequired();
                    city.Property(x => x.Code).HasColumnName("CityCode").IsRequired();
                    city.Property(x => x.Surtax).HasColumnName("CitySurtax").IsRequired();
                }
            );
            
            builder.OwnsOne(
                o => o.Company,
                company =>
                {
                    company.Property(x => x.Oib).HasColumnName("CompanyOib").IsRequired();
                    company.Property(x => x.Name).HasColumnName("CompanyName").IsRequired();
                    company.Property(x => x.Email).HasColumnName("CompanyEmail").IsRequired();
                    company.Property(x => x.Street).HasColumnName("CompanyStreet").IsRequired();
                    company.Property(x => x.City).HasColumnName("CompanyCity").IsRequired();

                    // ReSharper disable once HeapView.BoxingAllocation
                    company.HasIndex(x => x.Oib).IsUnique();
                }
            );
            
            builder.OwnsOne(
                o => o.Salary,
                salary =>
                {
                    salary.Property(x => x.Amount).HasColumnName("SalaryAmount").IsRequired();
                    salary.Property(x => x.Currency).HasColumnName("SalaryCurrency").IsRequired();
                    salary.Property(x => x.NonTaxableAmount).HasColumnName("SalaryNonTaxableAmount").IsRequired();
                    salary.Property(x => x.HealthInsuranceContribution).HasColumnName("SalaryHealthInsuranceContribution").IsRequired();
                    salary.Property(x => x.WorkSafetyContribution).HasColumnName("SalaryWorkSafetyContribution").IsRequired();
                    salary.Property(x => x.EmploymentContribution).HasColumnName("SalaryEmploymentContribution").IsRequired();
                    salary.Property(x => x.PensionPillar1Contribution).HasColumnName("SalaryPensionPillar1Contribution").IsRequired();
                    salary.Property(x => x.PensionPillar2Contribution).HasColumnName("SalaryPensionPillar2Contribution").IsRequired();
                }
            );
            
            builder.OwnsOne(
                o => o.Dividend,
                dividend =>
                {
                    dividend.Property(x => x.DividendTax).HasColumnName("DividendTax").IsRequired();
                }
            );

            builder.HasOne(s => s.User)
                .WithOne(u => u.UserSettings)
                // ReSharper disable once HeapView.BoxingAllocation
                .HasForeignKey<UserSettings>(x => x.UserId);
        }
    }
}