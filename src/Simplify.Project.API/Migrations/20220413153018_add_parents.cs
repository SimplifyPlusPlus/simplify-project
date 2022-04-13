using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplify.Project.API.Migrations
{
    public partial class add_parents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Entrances_EntranceId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Entrances_Houses_HouseId",
                table: "Entrances");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Estates_EstateId",
                table: "Houses");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstateId",
                table: "Houses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "HouseId",
                table: "Entrances",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EntranceId",
                table: "Apartments",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "EntranceId",
                table: "ApartmentRelations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Entrances_EntranceId",
                table: "Apartments",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Entrances_Houses_HouseId",
                table: "Entrances",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Estates_EstateId",
                table: "Houses",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.DropForeignKey(
                name: "FK_Apartments_Entrances_EntranceId",
                table: "Apartments");

            migrationBuilder.DropForeignKey(
                name: "FK_Entrances_Houses_HouseId",
                table: "Entrances");

            migrationBuilder.DropForeignKey(
                name: "FK_Houses_Estates_EstateId",
                table: "Houses");

            migrationBuilder.AlterColumn<Guid>(
                name: "EstateId",
                table: "Houses",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "HouseId",
                table: "Entrances",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntranceId",
                table: "Apartments",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<Guid>(
                name: "EntranceId",
                table: "ApartmentRelations",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Apartments_Entrances_EntranceId",
                table: "Apartments",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Entrances_Houses_HouseId",
                table: "Entrances",
                column: "HouseId",
                principalTable: "Houses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Houses_Estates_EstateId",
                table: "Houses",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id");
        }
    }
}
