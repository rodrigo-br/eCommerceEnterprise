using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECE.Cart.API.Migrations
{
    /// <inheritdoc />
    public partial class CartInitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TotalValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerCart", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsCart",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "varchar(100)", nullable: false),
                    ProductAmount = table.Column<int>(type: "int", nullable: false),
                    ProductValue = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ProductImage = table.Column<string>(type: "varchar(100)", nullable: false),
                    CartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsCart", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductsCart_CustomerCart_CartId",
                        column: x => x.CartId,
                        principalTable: "CustomerCart",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IDX_Customer",
                table: "CustomerCart",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductsCart_CartId",
                table: "ProductsCart",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsCart");

            migrationBuilder.DropTable(
                name: "CustomerCart");
        }
    }
}
