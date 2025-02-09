using System;
namespace ETicaretAPI.Application.Features.Queries.GetAllProducts
{
	public class GetAllProductsQueryResponse
	{
		public int TotalCount { get; set; }
		public object Products { get; set; }
	}
}

