using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace EventCatalogApi.Migrations
{
    public partial class EventInitial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "Month",
                table: "Catalog");

            migrationBuilder.RenameColumn(
                name: "Time",
                table: "Catalog",
                newName: "Zipcode");

            migrationBuilder.AddColumn<string>(
                name: "PictureUrl",
                table: "CatalogCategories",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "EndDate",
                table: "Catalog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EventDescription",
                table: "Catalog",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Catalog",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureUrl",
                table: "CatalogCategories");

            migrationBuilder.DropColumn(
                name: "EndDate",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "EventDescription",
                table: "Catalog");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Catalog");

            migrationBuilder.RenameColumn(
                name: "Zipcode",
                table: "Catalog",
                newName: "Time");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "Catalog",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "Catalog",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Month",
                table: "Catalog",
                nullable: false,
                defaultValue: "");
        }
    }
}
