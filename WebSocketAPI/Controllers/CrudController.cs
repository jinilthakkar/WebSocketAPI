namespace WebSocketAPI.Controllers
{
	using Microsoft.AspNetCore.Mvc;
	using System.Threading.Tasks;
	using WebSocketAPI.DTOs;
	using WebSocketAPI.Models;
	using WebSocketAPI.Services;

	[ApiController]
	[Route("api/[controller]")]
	public class CrudController : ControllerBase
	{
		private readonly CrudService _crudService;

		public CrudController(CrudService crudService)
		{
			_crudService = crudService;
		}

		[HttpGet("customers")]
		public async Task<IActionResult> GetAllCustomers() {
			try
			{
				var customerList = await _crudService.GetAllCustomers();

				if (customerList == null)
				{
					return NotFound();
				}
				return Ok(customerList);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpGet("customers/{customerId}")]
		public async Task<IActionResult> GetCustomerById(string customerId)
		{
			try
			{
				Customer? customer = await _crudService.GetCustomerById(customerId);

				if (customer == null)
					return NotFound();

				return Ok(customer);
			}
			catch (Exception ex) {
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost("customers")]
		public async Task<IActionResult> AddCustomer([FromBody] CustomerDTO dto)
		{
			try
			{
				bool added = await _crudService.AddCustomer(dto);
				return added ? Ok() : BadRequest();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost("customers/update")]
		public async Task<IActionResult> UpdateCustomer([FromBody] CustomerDTO dto)
		{
			try
			{
				bool updated = await _crudService.UpdateCustomer(dto);
				return updated ? Ok() : BadRequest();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpGet("order/{orderId}")]
		public async Task<IActionResult> GetOrderById(int orderId)
		{
			try
			{
				Order? order = await _crudService.GetOrderById(orderId);

				if (order == null)
					return NotFound();

				return Ok(order);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpGet("orders/{customerId}")]
		public async Task<IActionResult> GetOrdersByCustomerId(string customerId)
		{
			try
			{
				var orderList = await _crudService.GetOrdersByCustomerId(customerId);

				if (orderList == null)
				{
					return NotFound();
				}
				return Ok(orderList);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost("orders")]
		public async Task<IActionResult> AddOrder([FromBody] OrderDTO dto)
		{
			try
			{
				bool added = await _crudService.AddOrder(dto);
				return added ? Ok() : BadRequest();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}

		[HttpPost("orders/update")]
		public async Task<IActionResult> UpdateOrder([FromBody] OrderDTO dto)
		{
			try
			{
				bool updated = await _crudService.UpdateOrder(dto);
				return updated ? Ok() : BadRequest();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return StatusCode(500, "Internal Server Error");
			}
		}
	}
}
