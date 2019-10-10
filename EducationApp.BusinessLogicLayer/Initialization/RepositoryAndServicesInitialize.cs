using EducationApp.BusinessLogicLayer.Services;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EducationApp.BusinessLogicLayer.Initialization
{
    public static class RepositoryAndServicesInitialize
    {        
        public static void DbInitialize(IServiceCollection service,IConfiguration _configuration)
        {
            service.AddDbContext<ApplicationContext>(options => options.UseLazyLoadingProxies().UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            service.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
        }

        public static void ServicesInitialize(IServiceCollection service)
        {
            service.AddScoped<IUserService, UserService>();
            service.AddScoped<IAccountService, AccountService>();
            service.AddScoped<IPrintingEditionService, PrintingEditionService>();
            service.AddScoped<IAuthorService, AuthorService>();
            service.AddScoped<IOrderService, OrderService>();
        }

        public static void RepositoryInitialize(IServiceCollection service)
        {
            service.AddScoped<IUserRepository, UserRepository>();
            service.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            service.AddScoped<IAuthorRepository, AuthorRepository>();
            service.AddScoped<IAuthorInPrintingEditionRepository, AuthorInPrintingEditionRepository>();
            service.AddScoped<IOrderItemRepository, OrderItemRepository>();
            service.AddScoped<IOrderRepository, OrderRepository>();
            service.AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}
