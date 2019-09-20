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
using Stripe;
using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.DataAccessLayer.Initialization;
using EducationApp.BusinessLogicLayer.Models.Authorization;
using OrderService = EducationApp.BusinessLogicLayer.Services.OrderService;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddScoped<IOrderService, OrderService>();

            services.Configure<AuthTokenProviderOptions>(option=> 
            {
                option.JwtIssuer = Configuration["JwtIssuer"];
                option.JwtKey = Configuration["JwtKey"];
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
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

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
            });
            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddMvc();
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyAPI V1");
            });
        }
    }
}
