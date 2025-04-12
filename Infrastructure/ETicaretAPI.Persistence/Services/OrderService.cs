using System;
using ETicaretAPI.Application.Abstractions.Services;
using ETicaretAPI.Application.DTOs.Order;
using ETicaretAPI.Application.Repositories.IOrderRepositories;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI.Persistence.Services
{
	public class OrderService : IOrderService
	{
        private readonly IOrderWriteRepository _orderWriteRepository;
        private readonly IOrderReadRepository _orderReadRepository;

        public OrderService(IOrderWriteRepository orderWriteRepository, IOrderReadRepository orderReadRepository)
        {
            _orderWriteRepository = orderWriteRepository;
            _orderReadRepository = orderReadRepository;
        }

        public async Task CreateOrderAsync(CreateOrder createOrder)
        {
            var orderCode = (new Random().NextDouble() * 10000).ToString();
            orderCode = orderCode.Substring(orderCode.IndexOf(".") + 1, orderCode.Length - orderCode.IndexOf(".") - 1);

            await _orderWriteRepository.AddAsync(new()
            {
                Address = createOrder.Address,
                Id = Guid.Parse(createOrder.BasketId),
                Description = createOrder.Description,
                OrderCode = orderCode
            });
            await _orderWriteRepository.SaveAsync();
        }

        public async Task<ListOrder> GetAllOrdersAsync(int page, int size)
        {
            var query = _orderReadRepository.Table.Include(q => q.Basket)
                    .ThenInclude(q => q.User)
                    .Include(q => q.Basket)
                    .ThenInclude(q => q.BasketItems)
                    .ThenInclude(q => q.Product);

            var data = query.Skip(page * size).Take(size);

            return new()
            {
                TotalOrderCount = await query.CountAsync(),
                Orders = await data.Select(q => new
                {
                    CreatedDate = q.CreatedDate,
                    OrderCode = q.OrderCode,
                    TotalPrice = q.Basket.BasketItems.Sum(q => q.Product.Price * q.Quantity),
                    UserName = q.Basket.User.UserName
                }).ToListAsync()
            };
        }
    }
}

