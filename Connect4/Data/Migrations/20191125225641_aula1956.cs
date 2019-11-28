using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Connect4.Data.Migrations
{
    public partial class aula1956 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropForeignKey(
                name: "FK_Jogador_AspNetUsers_UsuarioId",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_Jogador_UsuarioId",
                table: "Jogador");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Jogador");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers",
                column: "JogadorId",
                unique: true,
                filter: "[JogadorId] IS NOT NULL");
                */
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "Jogador",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Jogador_UsuarioId",
                table: "Jogador",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_JogadorId",
                table: "AspNetUsers",
                column: "JogadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Jogador_AspNetUsers_UsuarioId",
                table: "Jogador",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
                */
        }
    }
}
