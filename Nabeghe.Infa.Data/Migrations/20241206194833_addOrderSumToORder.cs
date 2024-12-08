using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addOrderSumToORder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderSum",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderSum",
                table: "Orders");
        }
    }
}
