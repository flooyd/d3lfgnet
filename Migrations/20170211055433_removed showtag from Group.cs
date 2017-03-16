using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace d3lfg.Migrations
{
    public partial class removedshowtagfromGroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShowTag",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "HasGroup",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "ShowTag",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasGroup",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);
        }
    }
}
