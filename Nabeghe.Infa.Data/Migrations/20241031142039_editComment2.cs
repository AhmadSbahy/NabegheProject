using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class editComment2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_CourseComments_CourseCommentId",
                table: "CommentReplies");

            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_CourseCommentId",
                table: "CommentReplies");

            migrationBuilder.DropColumn(
                name: "CourseCommentId",
                table: "CommentReplies");

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_CommentId",
                table: "CommentReplies",
                column: "CommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_CourseComments_CommentId",
                table: "CommentReplies",
                column: "CommentId",
                principalTable: "CourseComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentReplies_CourseComments_CommentId",
                table: "CommentReplies");

            migrationBuilder.DropIndex(
                name: "IX_CommentReplies_CommentId",
                table: "CommentReplies");

            migrationBuilder.AddColumn<int>(
                name: "CourseCommentId",
                table: "CommentReplies",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentReplies_CourseCommentId",
                table: "CommentReplies",
                column: "CourseCommentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentReplies_CourseComments_CourseCommentId",
                table: "CommentReplies",
                column: "CourseCommentId",
                principalTable: "CourseComments",
                principalColumn: "Id");
        }
    }
}
