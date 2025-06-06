﻿using System;
using ETicaretAPI.Application.Abstractions.Hubs;
using ETicaretAPI.Application.Repositories.IProductRepositories;
using MediatR;

namespace ETicaretAPI.Application.Features.Commands.Product.CreateProduct
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest
                                                            ,CreateProductCommandResponse>
	{
        private readonly IProductWriteRepository _productWriteRepository;
        private readonly IProductHubService _productHubService;

        public CreateProductCommandHandler(IProductWriteRepository productWriteRepository,
                                           IProductHubService productHubService)
        {
            _productWriteRepository = productWriteRepository;
            _productHubService = productHubService;
        }

        public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await _productWriteRepository.AddAsync(new()
            {
                Name = request.Name,
                Stock = request.Stock,
                Price = request.Price
            });
            await _productWriteRepository.SaveAsync();
            await _productHubService.ProductAddedMessageAsync($"{request.Name} isminde ürün eklenmiştir");
            return new();
        }
    }
}

