﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nabeghe.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class addIsDeleteToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDelete",
                table: "Users",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDelete",
                table: "Users");
        }
    }
}
