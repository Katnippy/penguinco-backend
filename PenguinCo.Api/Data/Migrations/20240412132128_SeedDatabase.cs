using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PenguinCo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "StockItems",
                columns: new[] { "StockItemId", "Name" },
                values: new object[,]
                {
                    { 1, "Pingu" },
                    { 2, "Pinga" },
                    { 3, "Tux" },
                    { 4, "Tuxedosam" },
                    { 5, "Suica" },
                    { 6, "Donpen" },
                    { 7, "Pen Pen" },
                    { 8, "Private" },
                    { 9, "Skipper" },
                    { 10, "Kowalski" },
                    { 11, "Rico" }
                });

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "Address", "Name", "Updated" },
                values: new object[] { 1, "Shrewsbury, West Midlands, England", "PenguinCo Shrewsbury", new DateOnly(2024, 4, 12) });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "StockId", "Quantity", "StockItemId", "StoreId" },
                values: new object[,]
                {
                    { 1, 10, 1, 1 },
                    { 2, 5, 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "StockId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "StockId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 1);
        }
    }
}
