using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateContacUs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "ContactUs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "ContactUs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
