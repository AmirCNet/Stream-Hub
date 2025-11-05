using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StreamHub.Migrations
{
    /// <inheritdoc />
    public partial class CorreccionBD : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_IdUsuario",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "FechaFin",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "TipoSuscripcion",
                table: "Subscriptions",
                newName: "Plan");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Subscriptions",
                newName: "UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_IdUsuario",
                table: "Subscriptions",
                newName: "IX_Subscriptions_UsuarioId");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaExpiracion",
                table: "Subscriptions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_UsuarioId",
                table: "Subscriptions",
                column: "UsuarioId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Subscriptions_Users_UsuarioId",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "FechaExpiracion",
                table: "Subscriptions");

            migrationBuilder.RenameColumn(
                name: "UsuarioId",
                table: "Subscriptions",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "Plan",
                table: "Subscriptions",
                newName: "TipoSuscripcion");

            migrationBuilder.RenameIndex(
                name: "IX_Subscriptions_UsuarioId",
                table: "Subscriptions",
                newName: "IX_Subscriptions_IdUsuario");

            migrationBuilder.AddColumn<DateTime>(
                name: "FechaFin",
                table: "Subscriptions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_Subscriptions_Users_IdUsuario",
                table: "Subscriptions",
                column: "IdUsuario",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
