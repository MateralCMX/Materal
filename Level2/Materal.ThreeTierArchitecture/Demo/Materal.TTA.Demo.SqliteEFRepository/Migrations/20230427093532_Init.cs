using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.TTA.Demo.SqliteEFRepository.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestDomain",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "TEXT", nullable: false),
                    StringType = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    IntType = table.Column<int>(type: "INTEGER", maxLength: 100, nullable: false),
                    ByteType = table.Column<byte>(type: "INTEGER", nullable: false),
                    DecimalType = table.Column<decimal>(type: "TEXT", nullable: false),
                    EnumType = table.Column<byte>(type: "INTEGER", nullable: false),
                    DateTimeType = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestDomain", x => x.ID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TestDomain");
        }
    }
}
