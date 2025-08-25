using System.ComponentModel.DataAnnotations;

namespace WebSocketAPI.Models
{
	public class Customer
	{
		[Key]
		public string CustomerId { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
	}
}
