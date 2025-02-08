using System;
using System.Net;
using ETicaretAPI.Application.Abstractions.Storage;
using ETicaretAPI.Application.Repositories.IProductImageFileRepositories;
using ETicaretAPI.Application.Repositories.IProductRepositories;
using ETicaretAPI.Application.RequestParameters;
using ETicaretAPI.Application.ViewModels.Products;
using ETicaretAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductsController : ControllerBase
	{
		private readonly IProductReadRepository _productReadRepository;
		private readonly IProductWriteRepository _productWriteRepository;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly IStorageService _storageService;
		private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public ProductsController(IProductWriteRepository productWriteRepository,
            IProductReadRepository productReadRepository,
            IWebHostEnvironment webHostEnvironment,
            IStorageService storageService,
            IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _productWriteRepository = productWriteRepository;
            _productReadRepository = productReadRepository;
            _webHostEnvironment = webHostEnvironment;
            _storageService = storageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
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

		[HttpPost("[action]")]
		public async Task<IActionResult> Upload(string id)
		{
			List<(string fileName, string pathOrContainerName)> result
				= await _storageService.UploadAsync("photo-images", Request.Form.Files);

			Product product = await _productReadRepository.GetByIdAsync(id);


			await _productImageFileWriteRepository.AddRangeAsync(result.Select(q => new ProductImageFile
			{
				FileName = q.fileName,
				Path = q.pathOrContainerName,
				Storage = _storageService.StorageName,
				Products = new List<Product>() { product }
			}).ToList());

			await _productImageFileWriteRepository.SaveAsync();

			return Ok();
		}

		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> GetProductImages(string id)
		{
			Product? product = await _productReadRepository.Table
				.Include(q => q.ProductImageFiles)
				.FirstOrDefaultAsync(q => q.Id == Guid.Parse(id));

			return Ok(product.ProductImageFiles.Select(q => new
			{
				q.Path, // Ben local storage kullandım. Path'i ona göre düzenleyebiliriz
				q.FileName,
				q.Id
			}));
		}

		[HttpGet("[action]/{id}")]
		public async Task<IActionResult> DeleteProductImage(string id, string imageId)
		{
            Product? product = await _productReadRepository.Table
							.Include(q => q.ProductImageFiles)
							.FirstOrDefaultAsync(q => q.Id == Guid.Parse(id));
			ProductImageFile? productImageFile = product.ProductImageFiles
										.FirstOrDefault(q => q.Id == Guid.Parse(imageId));

			product.ProductImageFiles.Remove(productImageFile);
			_productWriteRepository.SaveAsync();
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

