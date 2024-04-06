using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UlukunShopAPI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig19 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Properties",
                table: "Products");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Properties",
                table: "Products",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
