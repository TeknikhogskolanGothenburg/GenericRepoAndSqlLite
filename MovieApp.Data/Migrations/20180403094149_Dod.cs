using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace MovieApp.Data.Migrations
{
    public partial class Dod : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Dod",
                table: "Actors",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActorsTemp",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Birthday = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                });

            //Copy all data from original table to temp table
            var sqlstr = "INSERT INTO ActorsTemp (Id, Birthday, Name, Nationality) " +
                         "SELECT Id, Birthday, Name, Nationality " +
                         "FROM Actors";
            migrationBuilder.Sql(sqlstr);

            migrationBuilder.DropTable(
                name: "Actors"
                );

            migrationBuilder.RenameTable(
                name: "ActorsTemp",
                newName: "Actors"
                );
        }

    }
}
