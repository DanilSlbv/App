using EducationApp.BusinessLogicLayer.Services;
using EducationApp.BusinessLogicLayer.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EducationApp.BusinessLogicLayer.Common
{
    public class ServicesInit
    {
        public ServicesInit(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPrintingEditionService, PrintingEditionService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IOrderService, OrderService>();
        }
    }
}
