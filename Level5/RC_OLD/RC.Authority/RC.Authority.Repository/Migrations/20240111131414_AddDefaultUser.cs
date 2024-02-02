using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RC.Authority.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AddDefaultUser : Migration
    {
        private static readonly string[] columns = ["ID", "Name", "Account", "Password", "CreateTime", "UpdateTime"];
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData("User", columns, new object[] {
                Guid.NewGuid(),
                "管理员",
                "Admin",
                $"Materal123456Materal".ToMd5_32Encode(),
                DateTime.Now,
                DateTime.Now
            });
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
