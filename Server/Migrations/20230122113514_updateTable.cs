using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WDeff.Server.Migrations
{
    /// <inheritdoc />
    public partial class updateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InputText",
                table: "Translations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "InputText",
                table: "Translations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
