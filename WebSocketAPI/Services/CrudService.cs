using WebSocketAPI.DTOs;
using WebSocketAPI.Models;

namespace WebSocketAPI.Services
{
	public class CrudService
	{
		private readonly DatabaseService _databaseService;
		public CrudService(DatabaseService databaseService) {
			_databaseService = databaseService;
		}

		public async Task<List<Customer>> GetAllCustomers() {
			return await _databaseService.GetAllCustomers();
		}

		public async Task<Customer?> GetCustomerById(string customerId)
		{
			return await _databaseService.GetCustomerByIdAsync(customerId);
		}

		public async Task<bool> AddCustomer(CustomerDTO dto)
		{
			if (dto == null || string.IsNullOrEmpty(dto.Name) || string.IsNullOrEmpty(dto.Email))
			{
				return false;
			}
			return await _databaseService.AddCustomerAsync(dto.Name, dto.Email);
		}

		public async Task<bool> UpdateCustomer(CustomerDTO dto)
		{
			if (dto == null || string.IsNullOrEmpty(dto.CustomerId)) return false;

			return await _databaseService.UpdateCustomerAsync(dto.CustomerId, dto.Name, dto.Email);
		}

		public async Task<Order?> GetOrderById(int orderId)
		{
			return await _databaseService.GetOrderByIdAsync(orderId);
		}

		public async Task<List<Order>?> GetOrdersByCustomerId(string customerId)
		{
			Customer? customer = await this.GetCustomerById(customerId);
			if (customer == null) {
				return null;
			}

			return await _databaseService.GetOrdersByCustomerIdAsync(customerId);
		}

		public async Task<bool> AddOrder(OrderDTO dto)
		{
			if (dto == null || string.IsNullOrEmpty(dto.CustomerId) || dto.TotalAmount == null || string.IsNullOrEmpty(dto.Status))
			{
				return false;
			}
			return await _databaseService.AddOrderAsync(dto.CustomerId, (decimal)dto.TotalAmount, dto.Status);
		}

		public async Task<bool> UpdateOrder(OrderDTO dto)
		{
			if (dto == null || dto.OrderId == null || string.IsNullOrEmpty(dto.Status)) return false;

			return await _databaseService.UpdateOrderAsync((int)dto.OrderId, dto.Status);
		}
	}
}
