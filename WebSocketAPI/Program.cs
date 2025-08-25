
using WebSocketAPI.Controllers;
using WebSocketAPI.Services;
using WebSocketAPI.Data;
using Microsoft.EntityFrameworkCore;
using WebSocketAPI.Hubs;

namespace WebSocketAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

						var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

						builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

						builder.Services.AddScoped<DatabaseService>();
						builder.Services.AddScoped<CrudService>();

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
						builder.Services.AddSignalR();

            var app = builder.Build();

						app.UseStaticFiles();

						app.UseRouting();

						app.MapGet("/", async context => {
								context.Response.ContentType = "text/html";
								await context.Response.SendFileAsync("wwwroot/index.html");
						});

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

						

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();
						app.MapHub<DataHub>("/datahub");

            app.Run();
        }
    }
}
