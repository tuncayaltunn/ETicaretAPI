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
				options.UseNpgsql(Configuration.ConnectionString)
			);

			services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

			services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

        } 
	}
}

