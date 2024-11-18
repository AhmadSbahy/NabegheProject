using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addComments2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CourseComments");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "CommentReplies");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CourseComments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "CommentReplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CourseComments_UserId",
                table: "CourseComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_UserId",
                table: "CommentReplies",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_Users_UserId",
                table: "CommentReplies",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_CourseComments_Users_UserId",
                table: "CourseComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_Users_UserId",
                table: "CommentReplies");

            migrationBuilder.DropForeignKey(
                name: "FK_CourseComments_Users_UserId",
                table: "CourseComments");

            migrationBuilder.DropIndex(
                name: "IX_CourseComments_UserId",
                table: "CourseComments");

            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_UserId",
                table: "CommentReplies");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CourseComments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CommentReplies");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CourseComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "CommentReplies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
