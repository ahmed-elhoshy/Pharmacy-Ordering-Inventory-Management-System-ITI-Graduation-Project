using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacySystem.InfastructureLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCartRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Medicines_MedicineId",
                table: "CartItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "CartItems",
                type: "decimal(5,2)",
                precision: 5,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems",
                column: "CartWarehouseId",
                principalTable: "CartWarehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Medicines_MedicineId",
                table: "CartItems",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Medicines_MedicineId",
                table: "CartItems");

            migrationBuilder.AlterColumn<decimal>(
                name: "Discount",
                table: "CartItems",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(5,2)",
                oldPrecision: 5,
                oldScale: 2);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems",
                column: "CartWarehouseId",
                principalTable: "CartWarehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Medicines_MedicineId",
                table: "CartItems",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
