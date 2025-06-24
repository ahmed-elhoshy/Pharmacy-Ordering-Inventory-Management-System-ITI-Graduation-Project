using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PharmacySystem.InfastructureLayer.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTwoRows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Pharmacies_PharmacyId",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_CartWarehouse_CartWarehouseId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Medicines_MedicineId",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartWarehouse_Cart_CartId",
                table: "CartWarehouse");

            migrationBuilder.DropForeignKey(
                name: "FK_CartWarehouse_WareHouses_WareHouseId",
                table: "CartWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartWarehouse",
                table: "CartWarehouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "CartWarehouse",
                newName: "CartWarehouses");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_CartWarehouse_WareHouseId",
                table: "CartWarehouses",
                newName: "IX_CartWarehouses_WareHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_CartWarehouse_CartId",
                table: "CartWarehouses",
                newName: "IX_CartWarehouses_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_MedicineId",
                table: "CartItems",
                newName: "IX_CartItems_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_CartWarehouseId",
                table: "CartItems",
                newName: "IX_CartItems_CartWarehouseId");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_PharmacyId",
                table: "Carts",
                newName: "IX_Carts_PharmacyId");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "Carts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Carts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartWarehouses",
                table: "CartWarehouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Id");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Pharmacies_PharmacyId",
                table: "Carts",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartWarehouses_Carts_CartId",
                table: "CartWarehouses",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartWarehouses_WareHouses_WareHouseId",
                table: "CartWarehouses",
                column: "WareHouseId",
                principalTable: "WareHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Pharmacies_PharmacyId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_CartWarehouses_Carts_CartId",
                table: "CartWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_CartWarehouses_WareHouses_WareHouseId",
                table: "CartWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartWarehouses",
                table: "CartWarehouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Carts");

            migrationBuilder.RenameTable(
                name: "CartWarehouses",
                newName: "CartWarehouse");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_CartWarehouses_WareHouseId",
                table: "CartWarehouse",
                newName: "IX_CartWarehouse_WareHouseId");

            migrationBuilder.RenameIndex(
                name: "IX_CartWarehouses_CartId",
                table: "CartWarehouse",
                newName: "IX_CartWarehouse_CartId");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_PharmacyId",
                table: "Cart",
                newName: "IX_Cart_PharmacyId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_MedicineId",
                table: "CartItem",
                newName: "IX_CartItem_MedicineId");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_CartWarehouseId",
                table: "CartItem",
                newName: "IX_CartItem_CartWarehouseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartWarehouse",
                table: "CartWarehouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Pharmacies_PharmacyId",
                table: "Cart",
                column: "PharmacyId",
                principalTable: "Pharmacies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_CartWarehouse_CartWarehouseId",
                table: "CartItem",
                column: "CartWarehouseId",
                principalTable: "CartWarehouse",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Medicines_MedicineId",
                table: "CartItem",
                column: "MedicineId",
                principalTable: "Medicines",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartWarehouse_Cart_CartId",
                table: "CartWarehouse",
                column: "CartId",
                principalTable: "Cart",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartWarehouse_WareHouses_WareHouseId",
                table: "CartWarehouse",
                column: "WareHouseId",
                principalTable: "WareHouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
