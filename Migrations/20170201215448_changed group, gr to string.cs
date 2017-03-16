using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace d3lfg.Migrations
{
    public partial class changedgroupgrtostring : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "GRLevel",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "GRLevel",
                table: "Groups",
                nullable: true);
        }
    }
}
