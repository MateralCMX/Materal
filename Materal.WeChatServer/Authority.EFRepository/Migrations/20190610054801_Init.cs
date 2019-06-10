using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Authority.EFRepository.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActionAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    ActionGroupCode = table.Column<string>(maxLength: 100, nullable: false),
                    Remark = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionAuthority", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "APIAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIAuthority", x => x.ID);
                    table.ForeignKey(
                        name: "FK_APIAuthority_APIAuthority_ParentID",
                        column: x => x.ParentID,
                        principalTable: "APIAuthority",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    IsDelete = table.Column<bool>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: true),
                    Code = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Role_Role_ParentID",
                        column: x => x.ParentID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Account = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(maxLength: 32, nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Sex = table.Column<byte>(type: "tinyint", nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: false),
                    Email = table.Column<string>(nullable: false),
                    IsDelete = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "WebMenuAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    Code = table.Column<string>(maxLength: 100, nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Style = table.Column<string>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    Remark = table.Column<string>(nullable: false),
                    ParentID = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WebMenuAuthority", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WebMenuAuthority_WebMenuAuthority_ParentID",
                        column: x => x.ParentID,
                        principalTable: "WebMenuAuthority",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoleActionAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    ActionAuthorityID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleActionAuthority", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoleActionAuthority_ActionAuthority_ActionAuthorityID",
                        column: x => x.ActionAuthorityID,
                        principalTable: "ActionAuthority",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleActionAuthority_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleAPIAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    APIAuthorityID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAPIAuthority", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoleAPIAuthority_APIAuthority_APIAuthorityID",
                        column: x => x.APIAuthorityID,
                        principalTable: "APIAuthority",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleAPIAuthority_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRole",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    UserID = table.Column<Guid>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRole", x => x.ID);
                    table.ForeignKey(
                        name: "FK_UserRole_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRole_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleWebMenuAuthority",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    UpdateTime = table.Column<DateTime>(nullable: false),
                    RoleID = table.Column<Guid>(nullable: false),
                    WebMenuAuthorityID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleWebMenuAuthority", x => x.ID);
                    table.ForeignKey(
                        name: "FK_RoleWebMenuAuthority_Role_RoleID",
                        column: x => x.RoleID,
                        principalTable: "Role",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleWebMenuAuthority_WebMenuAuthority_WebMenuAuthorityID",
                        column: x => x.WebMenuAuthorityID,
                        principalTable: "WebMenuAuthority",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_APIAuthority_ParentID",
                table: "APIAuthority",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_Role_ParentID",
                table: "Role",
                column: "ParentID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleActionAuthority_ActionAuthorityID",
                table: "RoleActionAuthority",
                column: "ActionAuthorityID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleActionAuthority_RoleID",
                table: "RoleActionAuthority",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAPIAuthority_APIAuthorityID",
                table: "RoleAPIAuthority",
                column: "APIAuthorityID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAPIAuthority_RoleID",
                table: "RoleAPIAuthority",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWebMenuAuthority_RoleID",
                table: "RoleWebMenuAuthority",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_RoleWebMenuAuthority_WebMenuAuthorityID",
                table: "RoleWebMenuAuthority",
                column: "WebMenuAuthorityID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_RoleID",
                table: "UserRole",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_UserRole_UserID",
                table: "UserRole",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_WebMenuAuthority_ParentID",
                table: "WebMenuAuthority",
                column: "ParentID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleActionAuthority");

            migrationBuilder.DropTable(
                name: "RoleAPIAuthority");

            migrationBuilder.DropTable(
                name: "RoleWebMenuAuthority");

            migrationBuilder.DropTable(
                name: "UserRole");

            migrationBuilder.DropTable(
                name: "ActionAuthority");

            migrationBuilder.DropTable(
                name: "APIAuthority");

            migrationBuilder.DropTable(
                name: "WebMenuAuthority");

            migrationBuilder.DropTable(
                name: "Role");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
