﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SaudiWorldCupHub.Migrations
{
    /// <inheritdoc />
    public partial class addingcode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Cities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Cities");
        }
    }
}
