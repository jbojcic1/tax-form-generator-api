using Microsoft.EntityFrameworkCore;
using TaxFormGeneratorApi.Bll;
using TaxFormGeneratorApi.Dal.Database.Configurations;

namespace TaxFormGeneratorApi.Dal.Database
{
    public class TaxFormGeneratorContext : DbContext
    {
        public TaxFormGeneratorContext(DbContextOptions<TaxFormGeneratorContext> options)
            : base(options)
        {}

        public DbSet<User> Users { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}