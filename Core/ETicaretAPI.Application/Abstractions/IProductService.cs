using System;
using ETicaretAPI.Domain.Entities;

namespace ETicaretAPI.Application.Abstractions
{
	public interface IProductService
	{
		public List<Product> GetAll();
	}
}

