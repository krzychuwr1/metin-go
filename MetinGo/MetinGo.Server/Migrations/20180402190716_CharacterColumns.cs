using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MetinGo.Server.Migrations
{
    public partial class CharacterColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Experience",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StatPoints",
                table: "Characters",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Fights",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CharacterId = table.Column<Guid>(nullable: false),
                    Experience = table.Column<int>(nullable: false),
                    MonsterId = table.Column<Guid>(nullable: false),
                    PlayerWon = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Fights_Characters_CharacterId",
                        column: x => x.CharacterId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Fights_Monsters_MonsterId",
                        column: x => x.MonsterId,
                        principalTable: "Monsters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fights_CharacterId",
                table: "Fights",
                column: "CharacterId");

            migrationBuilder.CreateIndex(
                name: "IX_Fights_MonsterId",
                table: "Fights",
                column: "MonsterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Fights");

            migrationBuilder.DropColumn(
                name: "Experience",
                table: "Characters");

            migrationBuilder.DropColumn(
                name: "StatPoints",
                table: "Characters");
        }
    }
}
