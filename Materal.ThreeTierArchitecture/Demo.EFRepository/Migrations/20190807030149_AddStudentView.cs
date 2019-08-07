using Microsoft.EntityFrameworkCore.Migrations;

namespace Demo.EFRepository.Migrations
{
    public partial class AddStudentView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
Create View [StudentInfoView]
AS
Select
    [dbo].[Student].[ID],
    [dbo].[Student].[CreateTime],
    [dbo].[Student].[UpdateTime],
    [dbo].[Student].[Name],
    [dbo].[Student].[Age],
    [dbo].[Student].[Sex],
    [dbo].[Student].[BelongClassID],
    [dbo].[Class].[Name] AS [BelongClassName]
From [dbo].[Student]
Inner Join [dbo].[Class] On [dbo].[Student].[BelongClassID] = [dbo].[Class].[ID]
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"Drop View [dbo].[StudentInfoView]");
        }
    }
}
