using System;
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
using ETicaretAPI.Application.Repositories.IFileRepositories;
using ETicaretAPI.Persistence.Repositories.FileRepositories;
using ETicaretAPI.Application.Repositories.IInvoiceFileRepositories;
using ETicaretAPI.Persistence.Repositories.InvoiceFileRepositories;
using ETicaretAPI.Application.Repositories.IProductImageFileRepositories;
using ETicaretAPI.Persistence.Repositories.ProductImageFileRepositories;
using ETicaretAPI.Domain.Entities.Identity;

namespace ETicaretAPI.Persistence
{
	public static class ServiceRegisration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddDbContext<ETicaretAPIDbContext>(options =>
				options.UseNpgsql(Configuration.ConnectionString)
			);

			services.AddIdentity<AppUser,AppRole>(options =>
			{
				options.Password.RequiredLength = 3;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
				options.User.RequireUniqueEmail = true;
			}).AddEntityFrameworkStores<ETicaretAPIDbContext>();

			services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();

			services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();

            services.AddScoped<IProductReadRepository, ProductReadRepository>();
            services.AddScoped<IProductWriteRepository, ProductWriteRepository>();

			services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();

            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

            services.AddScoped<IProductImageFileReadRepository, ProductImageFileReadRepository>();
            services.AddScoped<IProductImageFileWriteRepository, ProductImageFileWriteRepository>();

        } 
	}
}

