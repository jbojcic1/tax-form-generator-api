using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using TaxFormGeneratorApi.Dal;
using TaxFormGeneratorApi.Services;

namespace TaxFormGeneratorApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var jwtConfig = Configuration.GetSection("Auth").GetSection("JWT");
            var key = jwtConfig.GetValue<string>("Key");
            var issuer = jwtConfig.GetValue<string>("Issuer");
            var audience = jwtConfig.GetValue<string>("Audience");
            var tokenProvider = new TokenProviderService(key, issuer, audience);
            
            services.AddSingleton<ITokenProviderService>(tokenProvider);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = tokenProvider.GetValidationParameters();
                });
            
            services.AddMvc()
                .AddJsonOptions(options => {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2); 
            
            services.AddDbContext<TaxFormGeneratorContext>
                (options => options
                    .UseLazyLoadingProxies()
                    .UseNpgsql(Configuration.GetConnectionString("TaxFormGeneratorDb"))
                );

            services.AddTransient(typeof(IRepository<>), typeof(GenericRepository<>)); // TODO: should it be transient?            
            services.AddTransient(typeof(IAuthService), typeof(AuthService));         
            services.AddTransient(typeof(IPasswordHasher), typeof(PasswordHasher));         
            services.AddTransient(typeof(IAccountService), typeof(AccountService));         
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();

            // app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}