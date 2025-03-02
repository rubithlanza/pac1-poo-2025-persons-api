using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddAuditfieldToPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "persons",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "fist_name",
                table: "persons",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "dni",
                table: "persons",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "created_date",
                table: "persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_by",
                table: "persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "updated_date",
                table: "persons",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created_by",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "created_date",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "updated_by",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "updated_date",
                table: "persons");

            migrationBuilder.AlterColumn<string>(
                name: "last_name",
                table: "persons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "fist_name",
                table: "persons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "dni",
                table: "persons",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }
    }
}
