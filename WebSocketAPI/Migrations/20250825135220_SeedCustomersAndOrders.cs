using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebSocketAPI.Migrations
{
    /// <inheritdoc />
    public partial class SeedCustomersAndOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderId);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "CustomerId", "Email", "Name" },
                values: new object[,]
                {
                    { "123", "alice@gmail.com", "Alice Johnson" },
                    { "456", "bob@gmail.com", "Bob Smith" },
                    { "789", "john@gmail.com", "John Dow" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "OrderId", "CustomerId", "OrderDate", "Status", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "123", new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Completed", 100m },
                    { 2, "123", new DateTime(2025, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", 45m },
                    { 3, "456", new DateTime(2025, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Shipped", 78m },
                    { 4, "789", new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Processing", 33m }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
