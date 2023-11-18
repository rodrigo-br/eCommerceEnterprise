using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ECE.Cart.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductCartImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ProductImage",
                table: "ProductsCart",
                newName: "Image");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Image",
                table: "ProductsCart",
                newName: "ProductImage");
        }
    }
}
