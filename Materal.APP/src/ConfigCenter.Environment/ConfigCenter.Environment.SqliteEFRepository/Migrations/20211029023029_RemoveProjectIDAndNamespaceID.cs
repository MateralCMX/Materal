using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigCenter.Environment.SqliteEFRepository.Migrations
{
    public partial class RemoveProjectIDAndNamespaceID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NamespaceID",
                table: "ConfigurationItem");

            migrationBuilder.DropColumn(
                name: "ProjectID",
                table: "ConfigurationItem");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ConfigurationItem",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "ConfigurationItem",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "NamespaceID",
                table: "ConfigurationItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProjectID",
                table: "ConfigurationItem",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
