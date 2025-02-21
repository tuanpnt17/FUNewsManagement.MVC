using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FUNewsManagementSystem.RepositoryLayer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateNewArticleTitleName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Title",
                table: "NewsArticles",
                newName: "NewsTitle");

            migrationBuilder.AlterColumn<string>(
                name: "NewsContent",
                table: "NewsArticles",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewsTitle",
                table: "NewsArticles",
                newName: "Title");

            migrationBuilder.AlterColumn<string>(
                name: "NewsContent",
                table: "NewsArticles",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
