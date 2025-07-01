using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex10bis.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FactureId",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "DeliverySlotId",
                table: "DeliveryAssignment");

            migrationBuilder.CreateIndex(
                name: "IX_Facture_OrderId",
                table: "Facture",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignment_OrderId",
                table: "DeliveryAssignment",
                column: "OrderId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAssignment_Order_OrderId",
                table: "DeliveryAssignment",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Facture_Order_OrderId",
                table: "Facture",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAssignment_Order_OrderId",
                table: "DeliveryAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_Facture_Order_OrderId",
                table: "Facture");

            migrationBuilder.DropIndex(
                name: "IX_Facture_OrderId",
                table: "Facture");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryAssignment_OrderId",
                table: "DeliveryAssignment");

            migrationBuilder.AddColumn<int>(
                name: "FactureId",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DeliverySlotId",
                table: "DeliveryAssignment",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
