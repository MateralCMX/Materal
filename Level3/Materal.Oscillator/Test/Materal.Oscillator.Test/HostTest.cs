using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.Answers;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.Works;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.Oscillator.Test.RepositoryTest.SqliteEF
{
    [TestClass]
    public class HostTest : BaseTest
    {
        public override void AddServices(IServiceCollection services)
        {
        }
        /// <summary>
        /// ��ʼ����������
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task InitHostTestAsync()
        {
            IOscillatorHost host = GetServiceTest<IOscillatorHost>();
            Guid workID;
            Guid work2ID;
            Guid scheduleID;
            #region ����
            #region ���
            {
                AddWorkModel model = new()
                {
                    Name = "TestWork",
                    Description = "��������",
                    WorkData = new ConsoleWork() { Message = "������" }
                };
                workID = await host.AddWorkAsync(model);
                if (workID == Guid.Empty) Assert.Fail("�������ʧ��");
                model = new()
                {
                    Name = "TestWork2",
                    Description = "��������2",
                    WorkData = new ConsoleWork() { Message = "������" }
                };
                work2ID = await host.AddWorkAsync(model);
                if (work2ID == Guid.Empty) Assert.Fail("�������2ʧ��");
            }
            #endregion
            #region �޸�
            {
                (List<WorkDTO> dataList, _) = await host.GetWorkListAsync(new QueryWorkModel { Name = "TestWork", PageIndex = 1, PageSize = 10 });
                if (dataList.Count <= 0) Assert.Fail("��ѯ����ʧ��");
                WorkDTO dataInfo = await host.GetWorkInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("��ѯ����ʧ��");
                EditWorkModel model = new()
                {
                    ID = dataInfo.ID,
                    Name = dataInfo.Name,
                    Description = dataInfo.Description,
                    WorkData = dataInfo.WorkData
                };
                await host.EditWorkAsync(model);
                dataInfo = await host.GetWorkInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("��ѯ����ʧ��");
            }
            #endregion
            #endregion
            #region ������
            #region ���
            {
                AddScheduleModel model = new()
                {
                    Name = "TestSchedule",
                    Description = "���Ե���",
                    Answers = new()
                    {
                        new()
                        {
                            Name = "����������Ӧ",
                            AnswerData = new ConsoleAnswer() { Message = "����������Ӧ���" },
                            WorkEvent = "Success"
                        }
                    },
                    Plans = new()
                    {
                        new()
                        {
                            Name = "ִ��һ��",
                            Description = "ִ��һ��",
                            PlanTriggerData = new OneTimePlanTrigger { StartTime = DateTime.Now.AddSeconds(5) }
                        }
                    },
                    Works = new()
                    {
                        new() { WorkID = workID }
                    }
                };
                scheduleID = await host.AddScheduleAsync(model);
                if (scheduleID == Guid.Empty) Assert.Fail("��ӵ�����ʧ��");
            }
            #endregion
            #region �޸�
            {
                (List<ScheduleDTO> dataList, _) = await host.GetScheduleListAsync(new QueryScheduleModel { Name = "TestSchedule", PageIndex = 1, PageSize = 10 });
                if (dataList.Count <= 0) Assert.Fail("��ѯ������ʧ��");
                ScheduleDTO dataInfo = await host.GetScheduleInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("��ѯ������ʧ��");
                EditScheduleModel model = new()
                {
                    ID = dataInfo.ID,
                    Name = dataInfo.Name,
                    Description = dataInfo.Description,
                    Territory = dataInfo.Territory,
                    Enable = dataInfo.Enable,
                    Answers = new()
                    {
                        new()
                        {
                            Name = "����������Ӧ",
                            AnswerData = new ConsoleAnswer() { Message = "����������Ӧ���" },
                            WorkEvent = "Success"
                        },
                        new()
                        {
                            Name = "����������Ӧ2",
                            AnswerData = new ConsoleAnswer() { Message = "����������Ӧ���2" },
                            WorkEvent = "Success"
                        }
                    },
                    Plans = new()
                    {
                        new()
                        {
                            Name = "ִ��һ��",
                            Description = "ִ��һ��",
                            PlanTriggerData = new OneTimePlanTrigger { StartTime = DateTime.Now.AddSeconds(5) }
                        }
                    },
                    Works = new()
                    {
                        new() { WorkID = workID },
                        new() { WorkID = work2ID }
                    }
                };
                await host.EditScheduleAsync(model);
                dataInfo = await host.GetScheduleInfoAsync(dataList.First().ID);
                dataInfo.Validation();
                if (dataInfo == null) Assert.Fail("��ѯ������ʧ��");
            }
            #endregion
            #endregion
        }
        /// <summary>
        /// ��ʼ���ִ�����
        /// </summary>
        [TestMethod]
        public void InitRepositoryTest()
        {
            GetServiceTest<IOscillatorUnitOfWork>();
            GetServiceTest<IAnswerRepository>();
            GetServiceTest<IPlanRepository>();
            GetServiceTest<IScheduleRepository>();
            GetServiceTest<IScheduleWorkRepository>();
            GetServiceTest<IWorkRepository>();
        }
    }
}