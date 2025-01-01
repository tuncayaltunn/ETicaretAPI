using System;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Application.Repositories
{
	public interface IRepository<T> where T : class
	{
		DbSet<T> Table { get; }
	}
}

