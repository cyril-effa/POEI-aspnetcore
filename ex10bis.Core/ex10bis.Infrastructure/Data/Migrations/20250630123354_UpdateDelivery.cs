using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex10bis.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAssignmentDeliverySlot");

            migrationBuilder.DropTable(
                name: "DeliveryAssignment");

            migrationBuilder.CreateTable(
                name: "Delivery",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    LivreurId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Delivery_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryDeliverySlot",
                columns: table => new
                {
                    AssignmentsId = table.Column<int>(type: "int", nullable: false),
                    DeliverySlotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryDeliverySlot", x => new { x.AssignmentsId, x.DeliverySlotsId });
                    table.ForeignKey(
                        name: "FK_DeliveryDeliverySlot_DeliverySlot_DeliverySlotsId",
                        column: x => x.DeliverySlotsId,
                        principalTable: "DeliverySlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryDeliverySlot_Delivery_AssignmentsId",
                        column: x => x.AssignmentsId,
                        principalTable: "Delivery",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Delivery_OrderId",
                table: "Delivery",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryDeliverySlot_DeliverySlotsId",
                table: "DeliveryDeliverySlot",
                column: "DeliverySlotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryDeliverySlot");

            migrationBuilder.DropTable(
                name: "Delivery");

            migrationBuilder.CreateTable(
                name: "DeliveryAssignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    LivreurId = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAssignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryAssignment_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryAssignmentDeliverySlot",
                columns: table => new
                {
                    AssignmentsId = table.Column<int>(type: "int", nullable: false),
                    DeliverySlotsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryAssignmentDeliverySlot", x => new { x.AssignmentsId, x.DeliverySlotsId });
                    table.ForeignKey(
                        name: "FK_DeliveryAssignmentDeliverySlot_DeliveryAssignment_AssignmentsId",
                        column: x => x.AssignmentsId,
                        principalTable: "DeliveryAssignment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeliveryAssignmentDeliverySlot_DeliverySlot_DeliverySlotsId",
                        column: x => x.DeliverySlotsId,
                        principalTable: "DeliverySlot",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignment_OrderId",
                table: "DeliveryAssignment",
                column: "OrderId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignmentDeliverySlot_DeliverySlotsId",
                table: "DeliveryAssignmentDeliverySlot",
                column: "DeliverySlotsId");
        }
    }
}
