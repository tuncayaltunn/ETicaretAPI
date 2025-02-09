using System;
using ETicaretAPI.Application.Repositories.IProductRepositories;
using ETicaretAPI.Application.RequestParameters;
using MediatR;

namespace ETicaretAPI.Application.Features.Queries.Product.GetAllProducts
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest,
                                                            GetAllProductsQueryResponse>
	{
        private readonly IProductReadRepository _productReadRepository;

        public GetAllProductsQueryHandler(IProductReadRepository productReadRepository)
        {
            _productReadRepository = productReadRepository;
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

            return new()
            {
                Products = products,
                TotalCount = totalCount
            };
        }
    }
}

