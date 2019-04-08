using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaxFormGeneratorApi.Domain;

namespace TaxFormGeneratorApi.Dal.Configurations
{
    public class DividendJOPPDConfiguration : IEntityTypeConfiguration<DividendJOPPD>
    {
        public void Configure(EntityTypeBuilder<DividendJOPPD> builder)
        {
            builder.ToTable("DividendJOPPD");

            builder.Property(x => x.FormDate).IsRequired();
            builder.Property(x => x.PaymentDate).IsRequired();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.StartDate).IsRequired();
            builder.Property(x => x.EndDate).IsRequired();
        }
    }
}