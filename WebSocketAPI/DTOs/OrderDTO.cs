using System.Globalization;

namespace WebSocketAPI.DTOs
{
	public class OrderDTO
	{
		public int? OrderId { get; set; }
		public string? CustomerId { get; set; }
		public decimal? TotalAmount { get; set; }
		public string? Status	{ get; set; }
	}
}
