using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplify.Project.API.Migrations
{
    public partial class util : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRelations_Clients_ClientId",
                table: "ApartmentRelations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ApartmentRelations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "EntranceId",
                table: "ApartmentRelations",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRelations_EntranceId",
                table: "ApartmentRelations",
                column: "EntranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRelations_Clients_ClientId",
                table: "ApartmentRelations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRelations_Clients_ClientId",
                table: "ApartmentRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentRelations_EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.DropColumn(
                name: "EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.AlterColumn<Guid>(
                name: "ClientId",
                table: "ApartmentRelations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRelations_Clients_ClientId",
                table: "ApartmentRelations",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id");
        }
    }
}
