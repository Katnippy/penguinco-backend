using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PenguinCo.Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 1,
                column: "Updated",
                value: new DateOnly(2024, 4, 25));

            migrationBuilder.InsertData(
                table: "Stores",
                columns: new[] { "StoreId", "Address", "Name", "Updated" },
                values: new object[] { 2, "Lüderitz, ǁKaras Region, Namibia", "PenguinCo Namibia", new DateOnly(2024, 4, 25) });

            migrationBuilder.InsertData(
                table: "Stock",
                columns: new[] { "StockId", "Quantity", "StockItemId", "StoreId" },
                values: new object[,]
                {
                    { 3, 15, 8, 2 },
                    { 4, 15, 9, 2 },
                    { 5, 5, 10, 2 },
                    { 6, 20, 11, 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "StockId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "StockId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "StockId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Stock",
                keyColumn: "StockId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "Stores",
                keyColumn: "StoreId",
                keyValue: 1,
                column: "Updated",
                value: new DateOnly(2024, 4, 12));
        }
    }
}
