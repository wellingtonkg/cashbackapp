using CashBackApp.Database.Context;
using CashBackApp.Repositories.Abstract;
using CashBackApp.Repositories.Repositories;
using CashBackApp.Services.Abstract;
using CashBackApp.Services.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CashBackApp.DependencyInjection
{
    public static class Startup
    {
        public static void Start(IServiceCollection services)
        {
            //REPOSITORIES
            services.AddScoped(typeof(IEntityBaseRepositoryAsync<>), typeof(EntityBaseRepositoryAsync<>));
            services.AddScoped(typeof(ICashbackSettingsRepositoryAsync), typeof(CashbackSettingsRepositoryAsync));
            services.AddScoped(typeof(ICompanyRepositoryAsync), typeof(CompanyRepositoryAsync));
            services.AddScoped(typeof(ICustomerRepositoryAsync), typeof(CustomerRepositoryAsync));
            services.AddScoped(typeof(IOrderRepositoryAsync), typeof(OrderRepositoryAsync));
            services.AddScoped(typeof(IOrderItemRepositoryAsync), typeof(OrderItemRepositoryAsync));
            services.AddScoped(typeof(IProductRepositoryAsync), typeof(ProductRepositoryAsync));

            //SERVICES
            services.AddScoped(typeof(IEntityBaseServiceAsync<>), typeof(EntityBaseServiceAsync<>));
            services.AddScoped(typeof(ICashbackSettingsServiceAsync), typeof(CashbackSettingsServiceAsync));
            services.AddScoped(typeof(ICompanyServiceAsync), typeof(CompanyServiceAsync));
            services.AddScoped(typeof(ICustomerServiceAsync), typeof(CustomerServiceAsync));
            services.AddScoped(typeof(IOrderServiceAsync), typeof(OrderServiceAsync));
            services.AddScoped(typeof(IOrderItemServiceAsync), typeof(OrderItemServiceAsync));
            services.AddScoped(typeof(IProductServiceAsync), typeof(ProductServiceAsync));

            //Tempory Database
            services.AddDbContext<Context>(options => options.UseInMemoryDatabase("Cashback", null));
        }
    }
}
