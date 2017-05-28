using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Movies.Migrations
{
    public partial class posters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Poster",
                table: "Movies",
                newName: "PosterPortrait");

            migrationBuilder.AddColumn<string>(
                name: "PosterLandscape",
                table: "Movies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PosterLandscape",
                table: "Movies");

            migrationBuilder.RenameColumn(
                name: "PosterPortrait",
                table: "Movies",
                newName: "Poster");
        }
    }
}
