using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddFamilyGroupRe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "name",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "familyGroup");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "familyGroup");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "familyGroup",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_date",
                table: "familyGroup",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "familyGroup",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "familyGroup",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_date",
                table: "familyGroup",
                type: "TEXT",
                nullable: true);
        }
    }
}
