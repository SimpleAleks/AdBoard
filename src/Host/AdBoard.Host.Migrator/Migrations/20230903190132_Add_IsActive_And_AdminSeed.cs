using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AdBoard.Host.Migrator.Migrations
{
    /// <inheritdoc />
    public partial class Add_IsActive_And_AdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Advert",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "Login", "Name", "Password", "RegisteredTime", "Role" },
                values: new object[] { new Guid("b430cf1b-f797-462a-9f53-34fe82476dcf"), "login", "Ametusik", "5F4DCC3B5AA765D61D8327DEB882CF99", new DateTime(2023, 9, 3, 19, 1, 32, 99, DateTimeKind.Utc).AddTicks(8947), "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("b430cf1b-f797-462a-9f53-34fe82476dcf"));

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Advert");
        }
    }
}
