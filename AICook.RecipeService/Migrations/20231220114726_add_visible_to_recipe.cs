using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AICook.RecipeService.Migrations
{
    /// <inheritdoc />
    public partial class add_visible_to_recipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Visible",
                table: "Recipe",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visible",
                table: "Recipe");
        }
    }
}
