using System;
using ETicaretAPI.Application.Abstractions;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Persistence.Concretes
{
	public class ProductService : IProductService
	{
        public List<Product> GetAll() => new()
        {
            new() { Id = Guid.NewGuid(), Name = "Product 1", Price = 100, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 2", Price = 200, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 3", Price = 300, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 4", Price = 400, Stock = 10 },
            new() { Id = Guid.NewGuid(), Name = "Product 5", Price = 500, Stock = 10 }
        };
    }
}

