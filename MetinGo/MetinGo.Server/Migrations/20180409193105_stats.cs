using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MetinGo.Server.Migrations
{
    public partial class stats : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Attack",
                table: "Fights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Defence",
                table: "Fights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hp",
                table: "Fights",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseAttack",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseDefence",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BaseMaxHP",
                table: "Characters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Attack",
                table: "Fights");

            migrationBuilder.DropColumn(
                name: "Defence",
                table: "Fights");

            migrationBuilder.DropColumn(
                name: "Hp",
                table: "Fights");

            migrationBuilder.DropColumn(
                name: "BaseAttack",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseDefence",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "BaseMaxHP",
                table: "Characters");
        }
    }
}
