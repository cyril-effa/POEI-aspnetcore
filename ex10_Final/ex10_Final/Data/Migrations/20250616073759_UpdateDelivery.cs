using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ex10_Final.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDelivery : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DeliveryAssignment_DeliverySlot_DeliverySlotId",
                table: "DeliveryAssignment");

            migrationBuilder.DropIndex(
                name: "IX_DeliveryAssignment_DeliverySlotId",
                table: "DeliveryAssignment");

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
                name: "IX_DeliveryAssignmentDeliverySlot_DeliverySlotsId",
                table: "DeliveryAssignmentDeliverySlot",
                column: "DeliverySlotsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeliveryAssignmentDeliverySlot");

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryAssignment_DeliverySlotId",
                table: "DeliveryAssignment",
                column: "DeliverySlotId");

            migrationBuilder.AddForeignKey(
                name: "FK_DeliveryAssignment_DeliverySlot_DeliverySlotId",
                table: "DeliveryAssignment",
                column: "DeliverySlotId",
                principalTable: "DeliverySlot",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
