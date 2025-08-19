using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Books_BookAggregateId",
                table: "BookAuthors");

            migrationBuilder.DropIndex(
                name: "IX_BookAuthors_BookAggregateId",
                table: "BookAuthors");

            migrationBuilder.DropColumn(
                name: "Book_Id",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "BookAggregateId",
                table: "BookAuthors");

            migrationBuilder.RenameColumn(
                name: "Book_Year",
                table: "Books",
                newName: "Year");

            migrationBuilder.RenameColumn(
                name: "Book_Title",
                table: "Books",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "Book_PublishedOn",
                table: "Books",
                newName: "PublishedOn");

            migrationBuilder.RenameColumn(
                name: "Book_LastModifiedBy",
                table: "Books",
                newName: "LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "Book_LastModified",
                table: "Books",
                newName: "LastModified");

            migrationBuilder.RenameColumn(
                name: "Book_Genre",
                table: "Books",
                newName: "Genre");

            migrationBuilder.RenameColumn(
                name: "Book_Description",
                table: "Books",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Book_CreatedBy",
                table: "Books",
                newName: "CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Book_Created",
                table: "Books",
                newName: "Created");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWID()");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "Created",
                table: "Authors",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "LastModified",
                table: "Authors",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Authors",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "LastModified",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Authors");

            migrationBuilder.RenameColumn(
                name: "Year",
                table: "Books",
                newName: "Book_Year");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Books",
                newName: "Book_Title");

            migrationBuilder.RenameColumn(
                name: "PublishedOn",
                table: "Books",
                newName: "Book_PublishedOn");

            migrationBuilder.RenameColumn(
                name: "LastModifiedBy",
                table: "Books",
                newName: "Book_LastModifiedBy");

            migrationBuilder.RenameColumn(
                name: "LastModified",
                table: "Books",
                newName: "Book_LastModified");

            migrationBuilder.RenameColumn(
                name: "Genre",
                table: "Books",
                newName: "Book_Genre");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Books",
                newName: "Book_Description");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Books",
                newName: "Book_CreatedBy");

            migrationBuilder.RenameColumn(
                name: "Created",
                table: "Books",
                newName: "Book_Created");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "Book_Id",
                table: "Books",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "BookAggregateId",
                table: "BookAuthors",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Authors",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}
