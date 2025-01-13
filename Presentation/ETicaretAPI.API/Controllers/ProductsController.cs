using System;
using ETicaretAPI.Application.Repositories.IProductRepositories;
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

        [HttpGet("{id}")]
		public async Task<IActionResult> Get(string id)
		{
			Product product = await _productReadRepository.GetByIdAsync(id);

			return Ok(product);
		}

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var product = _productReadRepository.GetAll();

            return Ok(product);
        }

        [HttpGet("corstest")]
        public IActionResult CorsTest()
        {
            return Ok("Hello CORS");
        }
    }
}

