namespace WebSocketAPI.Services
{
	using Microsoft.EntityFrameworkCore;
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using WebSocketAPI.Data;
	using WebSocketAPI.Models;

	public class DatabaseService
	{
		private readonly AppDbContext _db;
		public DatabaseService(AppDbContext db) {
			_db = db;
		}

		public async Task<List<Customer>> GetAllCustomers() {
			return await _db.Customers.ToListAsync();
		}

		public async Task<Customer?> GetCustomerByIdAsync(string customerId) {
			return await _db.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.CustomerId == customerId);
		}

		public async Task<bool> AddCustomerAsync(string name, string email)
		{
			Customer newCustomer = new Customer()
			{
				CustomerId = Guid.NewGuid().ToString(),
				Name = name,
				Email = email
			};

			var added = await _db.Customers.AddAsync(newCustomer);
			await _db.SaveChangesAsync();

			return added != null;
		}

		public async Task<bool> UpdateCustomerAsync(string customerId, string? name, string? email)
		{
			Customer? oldCustomer = await this.GetCustomerByIdAsync(customerId);

			if (oldCustomer == null) return false;

			var newCustomer = new Customer()
			{
				CustomerId = oldCustomer.CustomerId,
				Email = email ?? oldCustomer.Email,
				Name = name ?? oldCustomer.Name
			};

			var updated = _db.Customers.Update(newCustomer);

			await _db.SaveChangesAsync();
			return updated != null;
		}

		public async Task<Order?> GetOrderByIdAsync(int orderId) {
			return await _db.Orders.AsNoTracking().FirstOrDefaultAsync(o => o.OrderId == orderId);
		}

		public async Task<List<Order>> GetOrdersByCustomerIdAsync(string customerId) {
			return await _db.Orders
				.AsNoTracking()
				.Where(o => o.CustomerId == customerId)
				.ToListAsync();
		}

		public async Task<bool> AddOrderAsync(string customerId, decimal totalAmount, string status) {
			Customer? customer = await this.GetCustomerByIdAsync(customerId);

			if (customer == null) return false;

			Order newOrder = new Order(){ CustomerId = customerId, TotalAmount = totalAmount, Status = status };

			var added = await _db.Orders.AddAsync(newOrder);
			await _db.SaveChangesAsync();

			return added != null;
		}

		public async Task<bool> UpdateOrderAsync(int orderId, string status)
		{
			Order? oldOrder = await this.GetOrderByIdAsync(orderId);
			if (oldOrder == null) return false;

			Order newOrder = new Order() {
				OrderId = oldOrder.OrderId,
				CustomerId = oldOrder.CustomerId,
				TotalAmount = oldOrder.TotalAmount,
				OrderDate = oldOrder.OrderDate,
				Status = status ?? oldOrder.Status
			};

			var updated = _db.Orders.Update(newOrder);

			await _db.SaveChangesAsync();
			return updated != null;
		}
	}
}
