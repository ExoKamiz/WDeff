using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDeff.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputText",
                table: "Translations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageFrom",
                table: "Translations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LanguageTo",
                table: "Translations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputText",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "LanguageFrom",
                table: "Translations");

            migrationBuilder.DropColumn(
                name: "LanguageTo",
                table: "Translations");
        }
    }
}
