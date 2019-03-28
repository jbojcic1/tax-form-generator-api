using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaxFormGeneratorApi.Dal.Database;

namespace TaxFormGeneratorApi.Dal.Ioc
{
    public class Ioc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TaxFormGeneratorContext>
                (options => options.UseNpgsql(configuration.GetConnectionString("TaxFormGeneratorDb")));
        }
    }
}