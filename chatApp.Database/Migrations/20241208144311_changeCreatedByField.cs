using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace chatApp.Database.Migrations
{
    /// <inheritdoc />
    public partial class changeCreatedByField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "created_by",
                table: "chats",
                type: "text",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<Guid>(
                name: "created_by",
                table: "chats",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }
    }
}
