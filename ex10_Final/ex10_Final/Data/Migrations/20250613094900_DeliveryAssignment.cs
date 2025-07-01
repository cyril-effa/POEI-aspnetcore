using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex10_Final.Data.Migrations
{
    /// <inheritdoc />
    public partial class DeliveryAssignment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LivreurId",
                table: "DeliverySlot");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "DeliverySlot");

            migrationBuilder.CreateTable(
                name: "DeliveryAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeliverySlotId = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    LivreurId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAssignment_DeliverySlot_DeliverySlotId",
                        column: x => x.DeliverySlotId,
                        principalTable: "DeliverySlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignment_DeliverySlotId",
                table: "DeliveryAssignment",
                column: "DeliverySlotId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAssignment");

            migrationBuilder.AddColumn<string>(
                name: "LivreurId",
                table: "DeliverySlot",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "DeliverySlot",
                type: "int",
                nullable: true);
        }
    }
}
