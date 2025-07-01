using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex10_Final.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddedDureeLivraison : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DureeLivraison",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DureeLivraison",
                table: "Order");
        }
    }
}
