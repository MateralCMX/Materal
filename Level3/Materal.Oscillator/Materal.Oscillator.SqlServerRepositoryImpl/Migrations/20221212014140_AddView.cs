using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.Oscillator.SqlServerRepositoryImpl.Migrations
{
    /// <inheritdoc />
    public partial class AddView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
CREATE VIEW [AnswerView]
AS
SELECT   dbo.Answer.ID, dbo.Answer.Name, dbo.Answer.ScheduleID, dbo.Schedule.Name AS ScheduleName, 
                dbo.Schedule.Territory, dbo.Answer.WorkEvent, dbo.Answer.Enable, dbo.Answer.OrderIndex, dbo.Answer.Description, 
                dbo.Answer.AnswerType, dbo.Answer.AnswerData, dbo.Answer.CreateTime, dbo.Answer.UpdateTime
FROM      dbo.Answer INNER JOIN
                dbo.Schedule ON dbo.Answer.ScheduleID = dbo.Schedule.ID
GO
CREATE VIEW [PlanView]
AS
SELECT   dbo.[Plan].ID, dbo.[Plan].Name, dbo.[Plan].ScheduleID, dbo.Schedule.Name AS ScheduleName, dbo.Schedule.Territory, 
                dbo.[Plan].Enable, dbo.[Plan].Description, dbo.[Plan].PlanTriggerType, dbo.[Plan].PlanTriggerData, 
                dbo.[Plan].CreateTime, dbo.[Plan].UpdateTime
FROM      dbo.[Plan] INNER JOIN
                dbo.Schedule ON dbo.[Plan].ScheduleID = dbo.Schedule.ID
GO
CREATE VIEW [ScheduleWorkView]
AS
SELECT   dbo.ScheduleWork.ID, dbo.ScheduleWork.ScheduleID, dbo.Schedule.Name AS ScheduleName, dbo.Schedule.Territory, 
                dbo.ScheduleWork.WorkID, dbo.[Work].Name AS WorkName, dbo.[Work].WorkType, dbo.[Work].WorkData, 
                dbo.ScheduleWork.OrderIndex, dbo.ScheduleWork.SuccessEvent, dbo.ScheduleWork.FailEvent, 
                dbo.ScheduleWork.CreateTime, dbo.ScheduleWork.UpdateTime
FROM      dbo.ScheduleWork INNER JOIN
                dbo.Schedule ON dbo.ScheduleWork.ScheduleID = dbo.Schedule.ID INNER JOIN
                dbo.[Work] ON dbo.ScheduleWork.WorkID = dbo.[Work].ID
GO
CREATE VIEW [WorkEventView]
AS
SELECT   dbo.WorkEvent.Name, dbo.WorkEvent.Value, dbo.WorkEvent.ScheduleID, dbo.Schedule.Name AS ScheduleName, 
                dbo.Schedule.Territory, dbo.WorkEvent.Description, dbo.WorkEvent.CreateTime, dbo.WorkEvent.UpdateTime
FROM      dbo.WorkEvent INNER JOIN
                dbo.Schedule ON dbo.WorkEvent.ScheduleID = dbo.Schedule.ID
GO
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP VIEW [AnswerView]
GO
DROP VIEW [PlanView]
GO
DROP VIEW [ScheduleWorkView]
GO
DROP VIEW [WorkEventView]
GO
");
        }
    }
}
