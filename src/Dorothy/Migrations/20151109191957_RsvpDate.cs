using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Dorothy.Migrations
{
    public partial class RsvpDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateTime",
                table: "Rsvp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DateTime", table: "Rsvp");
        }
    }
}
