using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Simplify.Project.API.Migrations
{
    public partial class fix_hierarchy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.DropIndex(
                name: "IX_ApartmentRelations_EntranceId",
                table: "ApartmentRelations");

            migrationBuilder.DropColumn(
                name: "EntranceId",
                table: "ApartmentRelations");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "EntranceId",
                table: "ApartmentRelations",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ApartmentRelations_EntranceId",
                table: "ApartmentRelations",
                column: "EntranceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ApartmentRelations_Entrances_EntranceId",
                table: "ApartmentRelations",
                column: "EntranceId",
                principalTable: "Entrances",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
