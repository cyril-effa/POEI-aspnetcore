using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex10_Final.Data.Migrations
{
    /// <inheritdoc />
    public partial class ShippingDHL : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "ShippingCost",
                table: "Order",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingCost",
                table: "Order");
        }
    }
}
