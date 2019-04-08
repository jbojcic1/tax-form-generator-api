using Microsoft.EntityFrameworkCore;
using TaxFormGeneratorApi.Domain;
using TaxFormGeneratorApi.Dal.Configurations;

namespace TaxFormGeneratorApi.Dal
{
    public class TaxFormGeneratorContext : DbContext
    {
        public TaxFormGeneratorContext(DbContextOptions<TaxFormGeneratorContext> options)
            : base(options)
        {}

        public DbSet<User> Users { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        public DbSet<SalaryJOPPD> SalaryJOPPDs { get; set; }
        public DbSet<DividendJOPPD> DividendJOPPDs { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserSettingsConfiguration());
            modelBuilder.ApplyConfiguration(new SalaryJOPPDConfiguration());
            modelBuilder.ApplyConfiguration(new DividendJOPPDConfiguration());
        }
    }
}