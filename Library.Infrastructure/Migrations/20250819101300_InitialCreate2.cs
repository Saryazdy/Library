using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BookAggregateId",
                table: "BookAuthors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BookAuthors_BookAggregateId",
                table: "BookAuthors",
                column: "BookAggregateId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Books_BookAggregateId",
                table: "BookAuthors",
                column: "BookAggregateId",
                principalTable: "Books",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Books_BookAggregateId",
                table: "BookAuthors");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_BookAggregateId",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "BookAggregateId",
                table: "BookAuthors");
        }
    }
}
