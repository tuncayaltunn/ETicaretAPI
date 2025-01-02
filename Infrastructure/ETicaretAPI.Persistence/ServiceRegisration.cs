using System;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Application.Repositories.ICustomerRepositories;
using ETicaretAPI.Application.Repositories.IOrderRepositories;
using ETicaretAPI.Application.Repositories.IProductRepositories;
using ETicaretAPI.Persistence.Repositories;
using ETicaretAPI.Persistence.Repositories.CustomerRepositories;
using ETicaretAPI.Persistence.Repositories.OrderRepositories;
using ETicaretAPI.Persistence.Repositories.ProductRepositories;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistence
{
	public static class ServiceRegisration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddDbContext<ETicaretAPIDbContext>(options =>
				options.UseNpgsql(Configuration.ConnectionString),
				ServiceLifetime.Singleton
			);

			services.AddSingleton<ICustomerReadRepository, CustomerReadRepository>();
            services.AddSingleton<ICustomerWriteRepository, CustomerWriteRepository>();

			services.AddSingleton<IOrderReadRepository, OrderReadRepository>();
            services.AddSingleton<IOrderWriteRepository, OrderWriteRepository>();

            services.AddSingleton<IProductReadRepository, ProductReadRepository>();
            services.AddSingleton<IProductWriteRepository, ProductWriteRepository>();

        } 
	}
}

