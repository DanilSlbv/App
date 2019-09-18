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
using EducationApp.DataAccessLayer.Initialization;
using EducationApp.BusinessLogicLayer.Models.Authorization;

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
            DbInitialize dbInitialize = new DbInitialize(Configuration,services);
            dbInitialize.Initialize();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, BusinessLogicLayer.Services.AccountService>();
            services.AddScoped<IPrintingEditionService,PrintingEditionService>();
            services.AddScoped<IAuthorService,AuthorService>();

            services.AddScoped<IAccountService, BusinessLogicLayer.Services.AccountService>();

            //services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            AuthTokenProviderOptions authTokenProviderOptions = new AuthTokenProviderOptions();
            authTokenProviderOptions.JwtIssuer= Configuration["JwtIssuer"];
            authTokenProviderOptions.JwtKey = Configuration["JwtKey"];
            services.AddSingleton(authTokenProviderOptions);

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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authTokenProviderOptions.JwtKey)),

                    ValidateIssuer = true,
                    ValidIssuer = authTokenProviderOptions.JwtIssuer,

                    ValidateAudience = true,
                    ValidAudience = authTokenProviderOptions.JwtIssuer,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            }).AddCookie(options=> 
            {
                options.Cookie.HttpOnly = true;
                options.Cookie.SameSite = SameSiteMode.Lax;
            });
            //services.AddAuthorization(options => { 
            //     options.AddPolicy("admin", policy=>policy.RequireRole("admin"));
            //     options.AddPolicy("user", policy=>policy.RequireRole("user"));
            // });

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
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvc();
            //loggerFactory.AddFile(Path.Combine("C:\\Users\\Anuitex-78\\source\\repos\\EducationApp", "LoggerFile.txt"));
            //var logger = loggerFactory.CreateLogger("Error");
            //app.Run(async (context) =>
            //{
            //    logger.LogInformation("Processing request{0}", context.Request.Path);
            //});
        }
    }
}
