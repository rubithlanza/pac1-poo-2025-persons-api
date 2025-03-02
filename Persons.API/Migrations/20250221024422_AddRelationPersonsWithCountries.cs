using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persons.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationPersonsWithCountries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "country_id",
                table: "persons",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CountryEntityId",
                table: "countries",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_persons_country_id",
                table: "persons",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "IX_countries_CountryEntityId",
                table: "countries",
                column: "CountryEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_countries_countries_CountryEntityId",
                table: "countries",
                column: "CountryEntityId",
                principalTable: "countries",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_persons_countries_country_id",
                table: "persons",
                column: "country_id",
                principalTable: "countries",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_countries_countries_CountryEntityId",
                table: "countries");

            migrationBuilder.DropForeignKey(
                name: "FK_persons_countries_country_id",
                table: "persons");

            migrationBuilder.DropIndex(
                name: "IX_persons_country_id",
                table: "persons");

            migrationBuilder.DropIndex(
                name: "IX_countries_CountryEntityId",
                table: "countries");

            migrationBuilder.DropColumn(
                name: "country_id",
                table: "persons");

            migrationBuilder.DropColumn(
                name: "CountryEntityId",
                table: "countries");
        }
    }
}
