using EducationApp.DataAccessLayer.Entities;
using EducationApp.DataAccessLayer.Repositories;
using EducationApp.DataAccessLayer.Repositories.Interface;
using EducationApp.DataAcessLayer.AppContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace EducationApp.DataAccessLayer.Initialization
{
    public class DbInitialize
    {
        private readonly IConfiguration _configuration;
        private readonly IServiceCollection _services;
        public DbInitialize(IConfiguration configuration, IServiceCollection services)
        {
            _configuration = configuration;
            _services = services;
        }

        public void Initialize()
        {
            _services.AddDbContext<ApplicationContext>(options => options.UseLazyLoadingProxies().UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            _services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();

            _services.AddScoped<IUserRepository, UserRepository>();
            _services.AddScoped<IPrintingEditionRepository, PrintingEditionRepository>();
            _services.AddScoped<IAuthorRepository, AuthorRepository>();
            _services.AddScoped<IAuthorInPrintingEditionRepository, AuthorInPrintingEditionRepository>();
            _services.AddScoped<IOrderItemRepository, OrderItemRepository>();
            _services.AddScoped<IOrderRepository,OrderRepository>();
            _services.AddScoped<IPaymentRepository, PaymentRepository>();
        }
    }
}
