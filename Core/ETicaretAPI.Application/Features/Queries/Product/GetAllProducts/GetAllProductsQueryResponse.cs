﻿using System;
namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
	public class GetAllProductsQueryResponse
	{
		public int TotalProductCount { get; set; }
		public object Products { get; set; }
	}
}

