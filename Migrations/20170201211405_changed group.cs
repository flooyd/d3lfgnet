using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace d3lfg.Migrations
{
    public partial class changedgroup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BNet",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "MinParagon",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "WillCarry",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "Difficulty",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Paragon",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GRLevel",
                table: "Groups",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Difficulty",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "Paragon",
                table: "Groups");

            migrationBuilder.AddColumn<string>(
                name: "BNet",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MinParagon",
                table: "Groups",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Groups",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "WillCarry",
                table: "Groups",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "GRLevel",
                table: "Groups",
                nullable: false);
        }
    }
}
