using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace d3lfg.Migrations
{
    public partial class HasGroupind3Usermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowTag",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasGroup",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowTag",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "HasGroup",
                table: "AspNetUsers");
        }
    }
}
