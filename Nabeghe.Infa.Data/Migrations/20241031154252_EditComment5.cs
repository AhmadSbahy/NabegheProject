using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class EditComment5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_CourseComments_UserId",
                table: "CommentLikes");

            migrationBuilder.CreateIndex(
                name: "IX_CommentLikes_CommentId",
                table: "CommentLikes",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_CourseComments_CommentId",
                table: "CommentLikes",
                column: "CommentId",
                principalTable: "CourseComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLikes_CourseComments_CommentId",
                table: "CommentLikes");

            migrationBuilder.DropIndex(
                name: "IX_CommentLikes_CommentId",
                table: "CommentLikes");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLikes_CourseComments_UserId",
                table: "CommentLikes",
                column: "UserId",
                principalTable: "CourseComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
