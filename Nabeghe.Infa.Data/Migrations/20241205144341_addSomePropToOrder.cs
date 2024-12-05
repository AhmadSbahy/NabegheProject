using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addSomePropToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Authority",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RefId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Authority",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RefId",
                table: "Orders");
        }
    }
}
