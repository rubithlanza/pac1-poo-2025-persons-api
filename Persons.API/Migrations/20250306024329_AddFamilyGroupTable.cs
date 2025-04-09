using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyGroupTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "relationShip",
                table: "familyGroup",
                newName: "relation_ship");

            migrationBuilder.AddColumn<string>(
                name: "first_name",
                table: "familyGroup",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "last_name",
                table: "familyGroup",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "person_id",
                table: "familyGroup",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_familyGroup_person_id",
                table: "familyGroup",
                column: "person_id");

            migrationBuilder.AddForeignKey(
                name: "FK_familyGroup_persons_person_id",
                table: "familyGroup",
                column: "person_id",
                principalTable: "persons",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_familyGroup_persons_person_id",
                table: "familyGroup");

            migrationBuilder.DropIndex(
                name: "IX_familyGroup_person_id",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "first_name",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "last_name",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "person_id",
                table: "familyGroup");

            migrationBuilder.RenameColumn(
                name: "relation_ship",
                table: "familyGroup",
                newName: "relationShip");
        }
    }
}
