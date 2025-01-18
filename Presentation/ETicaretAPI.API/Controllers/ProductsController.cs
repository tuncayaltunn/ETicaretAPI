using System;
using System.Net;
using ETicaretAPI.Application.Repositories.IProductRepositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETicaretAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;

		public ProductsController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository)
		{
			_productWriteRepository = productWriteRepository;
			_productReadRepository = productReadRepository;
		}

		[HttpGet]
		public async Task<IActionResult> Get([FromQuery]Pagination pagination)
		{
			var totalCount = _productReadRepository.GetAll(false).Count();
			var products = _productReadRepository.GetAll(false)
				.Skip(pagination.Size * pagination.Page).Take(pagination.Size)
				.Select(p => new
			{
				p.Id,
				p.Name,
				p.Stock,
				p.Price,
				p.CreatedDate,
				p.UpdatedDate
			});
			
            return Ok(new
			{
				totalCount,
                products
            });
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			Product product = await _productReadRepository.GetByIdAsync(id, false);

			return Ok(product);
		}

		[HttpPost]
		public async Task<IActionResult> Post(VM_Create_Product model)
		{
			await _productWriteRepository.AddAsync(new()
			{
				Name = model.Name,
				Stock = model.Stock,
				Price = model.Price
			});
			await _productWriteRepository.SaveAsync();
			return StatusCode((int)HttpStatusCode.Created);
		}

		[HttpPut]
		public async Task<IActionResult> Put(VM_Update_Product model)
		{
			Product product = await _productReadRepository.GetByIdAsync(model.Id);
			product.Name = model.Name;
			product.Stock = model.Stock;
			product.Price = model.Price;
			await _productWriteRepository.SaveAsync();
			return Ok();
		}

		[HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
		{
			await _productWriteRepository.RemoveAsync(id);
			await _productWriteRepository.SaveAsync();

			return Ok();
		}

  //      [HttpGet("{id}")]
		//public async Task<IActionResult> Get(string id)
		//{
		//	Product product = await _productReadRepository.GetByIdAsync(id);

		//	return Ok(product);
		//}

  //      [HttpGet("getall")]
  //      public IActionResult GetAll()
  //      {
  //          var product = _productReadRepository.GetAll();

  //          return Ok(product);
  //      }

  //      [HttpGet("corstest")]
  //      public IActionResult CorsTest()
  //      {
  //          return Ok("Hello CORS");
  //      }
    }
}

