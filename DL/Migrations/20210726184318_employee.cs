using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class employee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventory_Stores_StoreFront",
                table: "Inventory");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventory_Inventory_InventoryID",
                table: "StoreInventory");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventory_Products_ProductID",
                table: "StoreInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreInventory",
                table: "StoreInventory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory");

            migrationBuilder.RenameTable(
                name: "StoreInventory",
                newName: "StoreInventories");

            migrationBuilder.RenameTable(
                name: "Inventory",
                newName: "Inventories");

            migrationBuilder.RenameIndex(
                name: "IX_StoreInventory_InventoryID",
                table: "StoreInventories",
                newName: "IX_StoreInventories_InventoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Inventory_StoreFront",
                table: "Inventories",
                newName: "IX_Inventories_StoreFront");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreInventories",
                table: "StoreInventories",
                columns: new[] { "ProductID", "InventoryID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories",
                column: "InventoryID");

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeID);
                    table.ForeignKey(
                        name: "FK_Employee_Customers_CustomerID",
                        column: x => x.CustomerID,
                        principalTable: "Customers",
                        principalColumn: "CustomerID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employee_CustomerID",
                table: "Employee",
                column: "CustomerID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventories_Stores_StoreFront",
                table: "Inventories",
                column: "StoreFront",
                principalTable: "Stores",
                principalColumn: "StoreNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_Inventories_InventoryID",
                table: "StoreInventories",
                column: "InventoryID",
                principalTable: "Inventories",
                principalColumn: "InventoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventories_Products_ProductID",
                table: "StoreInventories",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Inventories_Stores_StoreFront",
                table: "Inventories");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_Inventories_InventoryID",
                table: "StoreInventories");

            migrationBuilder.DropForeignKey(
                name: "FK_StoreInventories_Products_ProductID",
                table: "StoreInventories");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StoreInventories",
                table: "StoreInventories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Inventories",
                table: "Inventories");

            migrationBuilder.RenameTable(
                name: "StoreInventories",
                newName: "StoreInventory");

            migrationBuilder.RenameTable(
                name: "Inventories",
                newName: "Inventory");

            migrationBuilder.RenameIndex(
                name: "IX_StoreInventories_InventoryID",
                table: "StoreInventory",
                newName: "IX_StoreInventory_InventoryID");

            migrationBuilder.RenameIndex(
                name: "IX_Inventories_StoreFront",
                table: "Inventory",
                newName: "IX_Inventory_StoreFront");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StoreInventory",
                table: "StoreInventory",
                columns: new[] { "ProductID", "InventoryID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Inventory",
                table: "Inventory",
                column: "InventoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Inventory_Stores_StoreFront",
                table: "Inventory",
                column: "StoreFront",
                principalTable: "Stores",
                principalColumn: "StoreNumber",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventory_Inventory_InventoryID",
                table: "StoreInventory",
                column: "InventoryID",
                principalTable: "Inventory",
                principalColumn: "InventoryID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StoreInventory_Products_ProductID",
                table: "StoreInventory",
                column: "ProductID",
                principalTable: "Products",
                principalColumn: "ProductID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
