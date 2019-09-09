using System;
using System.IO;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using EducationApp.DataAccessLayer.Repositories;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using EducationApp.BusinessLogicLayer.Common;
using EducationApp.DataAccessLayer.Entities;


namespace EducationApp.PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public TimeSpan TimeSpan { get; private set; }

      
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = "Server=(localdb)\\MSSQLLocalDB; Database=EducationStoreDb; Trusted_Connection=True; MultipleActiveResultSets=True";
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString(connection)));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<SmtpClient>((serviceProvider) => {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                return new SmtpClient()
                {
                    Host = config.GetValue<string>("Email:Smpt:Host:"),
                    Port = config.GetValue<int>("Email:Smpt:Port:"),
                    Credentials = new NetworkCredential(
                        config.GetValue<string>("Email:Smpt:Username:"),
                        config.GetValue<string>("Email:Smpt:Password:")
                       )
                };
            });


            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequireUppercase = true;

                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30.0);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Expiration = TimeSpan.FromDays(100);
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc();
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddFile(Path.Combine("C:\\Users\\Anuitex-78\\source\\repos\\EducationApp", "LoggerFile.txt"));
            var logger = loggerFactory.CreateLogger("Error");
            app.Run(async (context) =>
            {
                logger.LogInformation("Processing request{0}", context.Request.Path);
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc();
        }
    }
}
