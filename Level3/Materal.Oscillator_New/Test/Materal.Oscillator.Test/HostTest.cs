using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.DTO;
using Materal.Oscillator.Abstractions.Models;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.Oscillator.PlanTriggers;
using Materal.Oscillator.SqliteRepository;
using Materal.Oscillator.Works;
using Materal.TTA.EFRepository;
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
        public async Task InitHostTest()
        {
            IOscillatorHost host = GetServiceTest<IOscillatorHost>();
            Guid workID;
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
            }
            #endregion
            #region �޸�
            {
                EditWorkModel model = new()
                {
                    ID = workID,
                    Name = "TestWork",
                    Description = "��������",
                    WorkData = new ConsoleWork() { Message = "������" }
                };
                await host.EditWorkAsync(model);
            }
            #endregion
            #region ��ѯ
            {
                (List<WorkDTO> data, _) = await host.GetWorkListAsync(new QueryWorkModel { Name = "TestWork", PageIndex = 1, PageSize = 10 });
                if (data.Count <= 0) Assert.Fail("��ѯ����ʧ��");
                WorkDTO work = await host.GetWorkInfoAsync(data.First().ID);
                if(work == null) Assert.Fail("��ѯ����ʧ��");
            }
            #endregion
            #endregion
            #region ������
            #region ���
            {
                AddScheduleModel model = new()
                {
                    Name = "TestWork",
                    Description = "��������",
                    Plans = new()
                    {
                        new()
                        {
                            Name = "ִ��һ��",
                            Description = "ִ��һ��",
                            PlanTriggerData = new OneTimePlanTrigger
                            {
                                StartTime = DateTime.Now.AddSeconds(5)
                            }
                        }
                    },
                    Wokrs = new()
                    {
                        new()
                        {
                            WorkID = workID,
                            FailEvent = "Fial",
                            SuccessEvent = "OK",
                        }
                    }
                };
                scheduleID = await host.AddScheduleAsync(model);
                if (scheduleID == Guid.Empty) Assert.Fail("��ӵ�����ʧ��");
            }
            #endregion
            #endregion
        }
        /// <summary>
        /// ��ʼ���ִ�����
        /// </summary>
        [TestMethod]
        public async Task InitRepositoryTest()
        {
            IMigrateHelper<OscillatorDBContext> migrateHelper = GetServiceTest<IMigrateHelper<OscillatorDBContext>>();
            await migrateHelper.MigrateAsync();
            GetServiceTest<IOscillatorUnitOfWork>();
            GetServiceTest<IAnswerRepository>();
            GetServiceTest<IPlanRepository>();
            GetServiceTest<IScheduleRepository>();
            GetServiceTest<IScheduleWorkRepository>();
            GetServiceTest<IWorkEventRepository>();
            GetServiceTest<IWorkRepository>();
        }
    }
}