using System.ComponentModel.DataAnnotations;

namespace WebSocketAPI.Models
{
	public class Order
	{
		[Key]
		public int OrderId { get; set; }
		[Required]
		required public string CustomerId { get; set; }
		public DateTime OrderDate { get; set; }
		public decimal TotalAmount { get; set; }
		public string Status { get; set; } = "Ordered";
	}
}
