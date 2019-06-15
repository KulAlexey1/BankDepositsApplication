using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BDA.Core;
using BDA.DAL.EF;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MVC
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
            services.AddScoped<IAccountOperationRepository, AccountOperationRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<ICurrencyConversionRepository, CurrencyConversionRepository>();
            services.AddScoped<IDepositorRepository, DepositorRepository>();
            services.AddScoped<IDepositorTermRepository, DepositorTermRepository>();
            services.AddScoped<IDepositRepository, DepositRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IIssuingAuthorityLocalityRepository, IssuingAuthorityLocalityRepository>();
            services.AddScoped<IIssuingAuthorityRepository, IssuingAuthorityRepository>();
            services.AddScoped<ILocalityRepository, LocalityRepository>();
            services.AddScoped<IPassportRepository, PassportRepository>();
            services.AddScoped<IPhoneNumberOperatorCodeRepository, PhoneNumberOperatorCodeRepository>();
            services.AddScoped<IPhoneNumberOperatorRepository, PhoneNumberOperatorRepository>();
            services.AddScoped<IPhoneNumberRepository, PhoneNumberRepository>();
            services.AddScoped<IStreetRepository, StreetRepository>();
            services.AddScoped<DbContext, BankDepositsContext>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services
                .AddDbContext<BankDepositsContext>(options => 
                options.UseSqlServer(Configuration.GetConnectionString("BankDepositsDatabase")));

            services.AddDefaultIdentity<ClaimsIdentity>();

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = "/Account/SignIn";
                    options.LogoutPath = "/Account/SignOut";
                });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                app.UseExceptionHandler("/Shared/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Depositors}/{action=Index}/{id?}");
            });
        }
    }
}
