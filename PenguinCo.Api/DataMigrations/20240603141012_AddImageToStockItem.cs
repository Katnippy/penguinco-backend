using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PenguinCo.Api.DataMigrations
{
    /// <inheritdoc />
    public partial class AddImageToStockItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stores",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stores",
                type: "nvarchar(75)",
                maxLength: 75,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "StockItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 1,
                column: "Image",
                value: "../../images/pingu.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 2,
                column: "Image",
                value: "../../images/pinga.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 3,
                column: "Image",
                value: "../../images/tux.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 4,
                column: "Image",
                value: "../../images/tuxedosam.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 5,
                column: "Image",
                value: "../../images/suica.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 6,
                column: "Image",
                value: "../../images/donpen.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 7,
                column: "Image",
                value: "../../images/penpen.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 8,
                column: "Image",
                value: "../../images/private.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 9,
                column: "Image",
                value: "../../images/skipper.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 10,
                column: "Image",
                value: "../../images/kowalski.jpg");

            migrationBuilder.UpdateData(
                table: "StockItems",
                keyColumn: "StockItemId",
                keyValue: 11,
                column: "Image",
                value: "../../images/rico.jpg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "StockItems");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Stores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(75)",
                oldMaxLength: 75);
        }
    }
}
