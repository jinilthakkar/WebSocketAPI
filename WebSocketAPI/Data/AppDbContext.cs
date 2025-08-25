using Microsoft.EntityFrameworkCore;
using WebSocketAPI.Models;

namespace WebSocketAPI.Data
{

	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options): base(options) {}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Order> Orders { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Customer>()
			.HasData(
					new Customer { CustomerId = "123", Name = "Alice Johnson", Email = "alice@gmail.com" },
					new Customer { CustomerId = "456", Name = "Bob Smith", Email = "bob@gmail.com" },
					new Customer { CustomerId = "789", Name = "John Dow", Email = "john@gmail.com" }
			);

			modelBuilder.Entity<Order>()
				.HasData(
					new Order { OrderId = 1, CustomerId = "123", OrderDate = DateTime.Parse("2025-08-01"), TotalAmount = 100, Status = "Completed" },
					new Order { OrderId = 2, CustomerId = "123", OrderDate = DateTime.Parse("2025-08-05"), TotalAmount = 45, Status = "Processing" },
					new Order { OrderId = 3, CustomerId = "456", OrderDate = DateTime.Parse("2025-08-03"), TotalAmount = 78, Status = "Shipped" },
					new Order { OrderId = 4, CustomerId = "789", OrderDate = DateTime.Parse("2025-08-10"), TotalAmount = 33, Status = "Processing" }
				);
		}

	}
}
