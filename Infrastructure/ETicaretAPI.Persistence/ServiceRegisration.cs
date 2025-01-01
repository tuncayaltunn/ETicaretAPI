﻿using System;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Persistence.Concretes;
using Microsoft.Extensions.DependencyInjection;

namespace ETicaretAPI.Persistence
{
	public static class ServiceRegisration
	{
		public static void AddPersistenceServices(this IServiceCollection services)
		{
			services.AddSingleton<IProductService, ProductService>();
		} 
	}
}

