using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UlukunShopAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig15_added_features_to_entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "isThumbnail",
                table: "Files",
                type: "boolean",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "isThumbnail",
                table: "Files");
        }
    }
}
