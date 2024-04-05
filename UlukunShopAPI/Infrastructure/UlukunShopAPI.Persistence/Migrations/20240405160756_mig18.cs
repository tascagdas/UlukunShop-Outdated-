using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UlukunShopAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig18 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImageFilesId",
                table: "ProductProductImageFile");

            migrationBuilder.RenameColumn(
                name: "ProductImageFilesId",
                table: "ProductProductImageFile",
                newName: "ProductImagesId");

            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImagesId",
                table: "ProductProductImageFile",
                column: "ProductImagesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImagesId",
                table: "ProductProductImageFile");

            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "ProductImagesId",
                table: "ProductProductImageFile",
                newName: "ProductImageFilesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductProductImageFile_Files_ProductImageFilesId",
                table: "ProductProductImageFile",
                column: "ProductImageFilesId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
