using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Materal.Oscillator.SqliteRepositoryImpl.Migrations
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
SELECT   Answer.ID, Answer.Name, Answer.ScheduleID, Schedule.Name AS ScheduleName, 
                Schedule.Territory, Answer.WorkEvent, Answer.Enable, Answer.OrderIndex, Answer.Description, 
                Answer.AnswerType, Answer.AnswerData, Answer.CreateTime, Answer.UpdateTime
FROM      Answer INNER JOIN
                Schedule ON Answer.ScheduleID = Schedule.ID;
CREATE VIEW [PlanView]
AS
SELECT   [Plan].ID, [Plan].Name, [Plan].ScheduleID, Schedule.Name AS ScheduleName, Schedule.Territory, 
                [Plan].Enable, [Plan].Description, [Plan].PlanTriggerType, [Plan].PlanTriggerData, 
                [Plan].CreateTime, [Plan].UpdateTime
FROM      [Plan] INNER JOIN
                Schedule ON [Plan].ScheduleID = Schedule.ID;
CREATE VIEW [ScheduleWorkView]
AS
SELECT   ScheduleWork.ID, ScheduleWork.ScheduleID, Schedule.Name AS ScheduleName, Schedule.Territory, 
                ScheduleWork.WorkID, [Work].Name AS WorkName, [Work].WorkType, [Work].WorkData, 
                ScheduleWork.OrderIndex, ScheduleWork.SuccessEvent, ScheduleWork.FailEvent, 
                ScheduleWork.CreateTime, ScheduleWork.UpdateTime
FROM      ScheduleWork INNER JOIN
                Schedule ON ScheduleWork.ScheduleID = Schedule.ID INNER JOIN
                [Work] ON ScheduleWork.WorkID = [Work].ID;
CREATE VIEW [WorkEventView]
AS
SELECT   WorkEvent.Name, WorkEvent.Value, WorkEvent.ScheduleID, Schedule.Name AS ScheduleName, 
                Schedule.Territory, WorkEvent.Description, WorkEvent.CreateTime, WorkEvent.UpdateTime
FROM      WorkEvent INNER JOIN
                Schedule ON WorkEvent.ScheduleID = Schedule.ID;
");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DROP VIEW [AnswerView];
DROP VIEW [PlanView];
DROP VIEW [ScheduleWorkView];
DROP VIEW [WorkEventView];
");
        }
    }
}
