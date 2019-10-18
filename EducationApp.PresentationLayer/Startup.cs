using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Stripe;
using EducationApp.BusinessLogicLayer.Models.Payments;
using EducationApp.BusinessLogicLayer.Models.Authorization;
using Swashbuckle.AspNetCore.Swagger;
using EducationApp.BusinessLogicLayer.Initialization;
using EducationApp.BusinessLogicLayer.Helpers;

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
            RepositoryAndServicesInitialize.DbInitialize(services,Configuration);
            RepositoryAndServicesInitialize.ServicesInitialize(services);
            RepositoryAndServicesInitialize.RepositoryInitialize(services);
            services.AddTransient<JwtHelper>();

            services.Configure<AuthTokenProviderOptionsModel>(option=> 
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
                options.Cookie.SameSite = SameSiteMode.None;
            });

                services.Configure<IdentityOptions>(options =>
                {
                    options.Password.RequiredLength = 6;

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
            services.Configure<StripeSettingsModel>(Configuration.GetSection("Stripe"));
            services.AddCors();
            services.AddMvc();
        }

       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(options =>
            options.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
            
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
