using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace Dorothy.Migrations
{
    public partial class HasEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasEmail",
                table: "Guest",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "HasEmail", table: "Guest");
        }
    }
}
