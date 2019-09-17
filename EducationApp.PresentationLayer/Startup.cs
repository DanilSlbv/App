using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.BusinessLogicLayer.Services;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.EntityFrameworkCore;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAccessLayer.Repositories;

namespace EducationApp.PresentationLayer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
             
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<ApplicationContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
                services.AddScoped<IAccountService, AccountService>();



            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JwtKey"])),

                    ValidateIssuer = true,
                    ValidIssuer = Configuration["JwtIssuer"],

                    ValidateAudience = true,
                    ValidAudience = Configuration["JwtIssuer"],

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }).AddCookie(options=> 
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
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
                    options.MinimumSameSitePolicy = SameSiteMode.Strict;
                    options.HttpOnly = Microsoft.AspNetCore.CookiePolicy.HttpOnlyPolicy.None;
                });
                services.AddMvc();
            
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();
            //loggerFactory.AddFile(Path.Combine("C:\\Users\\Anuitex-78\\source\\repos\\EducationApp", "LoggerFile.txt"));
            //var logger = loggerFactory.CreateLogger("Error");
            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("Processing request{0}", context.Request.Path);
            //});
        }
    }
}
