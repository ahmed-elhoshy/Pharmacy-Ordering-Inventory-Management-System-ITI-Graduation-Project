using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacySystem.InfastructureLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class FixCartCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems",
                column: "CartWarehouseId",
                principalTable: "CartWarehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_CartWarehouses_CartWarehouseId",
                table: "CartItems",
                column: "CartWarehouseId",
                principalTable: "CartWarehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
