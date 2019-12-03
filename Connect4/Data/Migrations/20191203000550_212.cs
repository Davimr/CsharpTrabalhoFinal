using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Connect4.Data.Migrations
{
    public partial class _212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_Jogos_JogoId",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_JogoId",
                table: "Jogador");

            migrationBuilder.DropColumn(
                name: "JogoId",
                table: "Jogador");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JogoId",
                table: "Jogador",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_JogoId",
                table: "Jogador",
                column: "JogoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_Jogos_JogoId",
                table: "Jogador",
                column: "JogoId",
                principalTable: "Jogos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
