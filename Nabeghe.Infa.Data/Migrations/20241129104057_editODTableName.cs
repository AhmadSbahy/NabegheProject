using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class editODTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrdersDetails_Courses_CourseId",
                table: "OrdersDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrdersDetails_Orders_OrderId",
                table: "OrdersDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrdersDetails",
                table: "OrdersDetails");

            migrationBuilder.RenameTable(
                name: "OrdersDetails",
                newName: "OrderDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersDetails_OrderId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrdersDetails_CourseId",
                table: "OrderDetails",
                newName: "IX_OrderDetails_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Courses_CourseId",
                table: "OrderDetails",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Courses_CourseId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Orders_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "OrderDetails",
                newName: "OrdersDetails");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrdersDetails",
                newName: "IX_OrdersDetails_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderDetails_CourseId",
                table: "OrdersDetails",
                newName: "IX_OrdersDetails_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrdersDetails",
                table: "OrdersDetails",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersDetails_Courses_CourseId",
                table: "OrdersDetails",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrdersDetails_Orders_OrderId",
                table: "OrdersDetails",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
