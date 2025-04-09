using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class FamilyGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "familyGroup",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "TEXT", nullable: false),
                    name = table.Column<string>(type: "TEXT", nullable: false),
                    relationShip = table.Column<string>(type: "TEXT", nullable: false),
                    created_by = table.Column<string>(type: "TEXT", nullable: true),
                    created_date = table.Column<string>(type: "TEXT", nullable: true),
                    updated_by = table.Column<string>(type: "TEXT", nullable: true),
                    updated_date = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_familyGroup", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "familyGroup");
        }
    }
}
