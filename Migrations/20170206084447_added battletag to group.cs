using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace d3lfg.Migrations
{
    public partial class addedbattletagtogroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BattleTag",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleTag",
                table: "Groups");
        }
    }
}
