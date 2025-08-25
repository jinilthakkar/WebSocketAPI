using Microsoft.AspNetCore.SignalR;
using WebSocketAPI.Services;

namespace WebSocketAPI.Hubs
{
	public class DataHub: Hub
	{
		private readonly CrudService _crudService;
		private PeriodicTimer periodicTimer;

		public DataHub(CrudService crudService) {
			_crudService = crudService;
			periodicTimer = new PeriodicTimer(TimeSpan.FromSeconds(3));
		}

		public async Task SubscribeToCustomerOrders(string customerId) {
			while (await periodicTimer.WaitForNextTickAsync())
			{
				try
				{
					var orders = await _crudService.GetOrdersByCustomerId(customerId);

					await Clients.Caller.SendAsync("Orders", orders);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"Error sending periodic update: {ex}");
				}
			}
		}

		public async Task SubscribeToCustomer(string customerId) {
			while (await periodicTimer.WaitForNextTickAsync())
			{
				try
				{
					var customer = await _crudService.GetCustomerById(customerId);

					await Clients.Caller.SendAsync("Customer", customer);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine($"Error sending periodic update: {ex}");
				}
			}
		}
	}
}
