using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Dal.Configurations
{
    public class SalaryJOPPDConfiguration : IEntityTypeConfiguration<SalaryJOPPD>
    {
        public void Configure(EntityTypeBuilder<SalaryJOPPD> builder)
        {
            builder.ToTable("SalaryJOPPD");

            builder.Property(x => x.FormDate).IsRequired();
            builder.Property(x => x.PaymentDate).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.SalaryMonth).IsRequired();
        }
    }
}