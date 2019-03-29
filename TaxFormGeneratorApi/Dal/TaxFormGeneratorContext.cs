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
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }
    }
}