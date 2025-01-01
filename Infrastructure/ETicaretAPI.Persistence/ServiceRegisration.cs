using System;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Persistence.Concretes;
using ETicaretAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistence
{
	public static class ServiceRegisration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddSingleton<IProductService, ProductService>();

			services.AddDbContext<ETicaretAPIDbContext>(options =>
				options.UseNpgsql(Configuration.ConnectionString)
			);
		} 
	}
}

