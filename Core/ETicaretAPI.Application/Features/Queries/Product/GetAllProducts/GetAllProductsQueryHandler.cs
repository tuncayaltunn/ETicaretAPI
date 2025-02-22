using System;
using ETicaretAPI.Application.Repositories.IProductRepositories;
using ETicaretAPI.Application.RequestParameters;
using MediatR;
using Microsoft.Extensions.Logging;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest,
                                                            GetAllProductsQueryResponse>
	{
        private readonly IProductReadRepository _productReadRepository;
        private readonly ILogger<GetAllProductsQueryHandler> _logger;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository,
                                          ILogger<GetAllProductsQueryHandler> logger)
        {
            _productReadRepository = productReadRepository;
            _logger = logger;
        }

        public async Task<GetAllProductsQueryResponse> Handle(GetAllProductsQueryRequest request,
                                                            CancellationToken cancellationToken)
        {
            var totalCount = _productReadRepository.GetAll(false).Count();
            var products = _productReadRepository.GetAll(false)
            .Skip(request.Size * request.Page).Take(request.Size)
            .Select(p => new
            {
                p.Id,
                p.Name,
                    p.Stock,
                    p.Price,
                    p.CreatedDate,
                    p.UpdatedDate
                }).ToList();
            _logger.LogInformation("Get All Products");
            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}

