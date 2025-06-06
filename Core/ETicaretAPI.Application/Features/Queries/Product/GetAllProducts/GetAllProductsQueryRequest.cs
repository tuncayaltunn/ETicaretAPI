﻿using System;
using ETicaretAPI.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
	public class GetAllProductsQueryRequest : IRequest<GetAllProductsQueryResponse>
	{
        //public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;

        public int Size { get; set; } = 5;
    }
}

