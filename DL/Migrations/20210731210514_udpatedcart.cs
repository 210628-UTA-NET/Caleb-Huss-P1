using Microsoft.EntityFrameworkCore.Migrations;

namespace DL.Migrations
{
    public partial class udpatedcart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StoreFront",
                table: "Carts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Carts_StoreFront",
                table: "Carts",
                column: "StoreFront");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Stores_StoreFront",
                table: "Carts",
                column: "StoreFront",
                principalTable: "Stores",
                principalColumn: "StoreNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Stores_StoreFront",
                table: "Carts");

            migrationBuilder.DropIndex(
                name: "IX_Carts_StoreFront",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "StoreFront",
                table: "Carts");
        }
    }
}
