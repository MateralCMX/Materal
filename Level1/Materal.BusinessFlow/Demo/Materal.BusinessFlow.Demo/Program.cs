using Materal.Abstractions;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.Models;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Extensions;
using Materal.BusinessFlow.SqliteRepository.Models;
using Materal.BusinessFlow.SqlServerRepository.Models;
using Materal.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Runtime.InteropServices;

namespace Materal.BusinessFlow.Demo
{
    public class Program
    {
        private static readonly IServiceProvider _serviceProvider;
        private static readonly User _userCMX = new()
        {
            ID = Guid.Parse("E0DEE933-BA60-417A-95DD-8992BC3087F0"),
            Name = "陈明旭"
        };
        private static readonly User _userCMX2 = new()
        {
            ID = Guid.Parse("E0DEE933-BA60-417A-95DD-8992BC3087F3"),
            Name = "陈明旭2"
        };
        private static readonly User _userWRD = new()
        {
            ID = Guid.Parse("E0DEE933-BA60-417A-95DD-8992BC3087F1"),
            Name = "王任达"
        };
        private static readonly User _userFBW = new()
        {
            ID = Guid.Parse("E0DEE933-BA60-417A-95DD-8992BC3087F2"),
            Name = "方宝文"
        };
        private static readonly DataModel _testDataModel = new()
        {
            ID = Guid.Parse("6E73FE4E-D481-41FE-BCA0-976A92D7F9C4"),
            Name = "测试数据模型"
        };
        private static readonly List<DataModelField> _testDataModelFields = new();
        private static readonly FlowTemplate _testFlowTemplate = new()
        {
            ID = Guid.Parse("35F00828-0491-4F46-AFAF-E01796FE1CC7"),
            Name = "请假流程模版",
            DataModelID = _testDataModel.ID
        };
        private static readonly List<Step> _steps = new();
        private static readonly Dictionary<Guid, List<Node>> _nodes = new();
        private static readonly ILogger<Program>? _logger;
        static Program()
        {
            MateralConfig.PageStartNumber = 1;
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger();
            services.AddBusinessFlow();
            //services.AddBusinessFlowSqliteRepository(new SqliteConfigModel { Source = "BusinessFlow.db" });
            services.AddBusinessFlowSqlServerRepository(new SqlServerConfigModel
            {
                Address = "175.27.194.19",
                Port = "1433",
                Name = "BusinessFlowDB",
                UserID = "sa",
                Password = "XMJry@456",
                TrustServerCertificate = true
            });
            _serviceProvider = services.BuildServiceProvider();
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole", null, new Dictionary<LogLevel, ConsoleColor>
                {
                    [LogLevel.Error] = ConsoleColor.DarkRed
                });
                option.AddAllTargetRule(LogLevel.Information);
            });
            _logger = _serviceProvider.GetService<ILogger<Program>>();
        }
        public static async Task Main()
        {
            try
            {
                await InitAsync();
                await BusinessFlowExcuteAsync();
            }
            catch (BusinessFlowException ex)
            {
                _logger?.LogError(ex, "业务流出错");
            }
            WriteTestInfo("测试程序执行完毕，按任意键退出...");
            Console.ReadKey();
        }
        /// <summary>
        /// 业务流执行
        /// </summary>
        /// <returns></returns>
        private static async Task BusinessFlowExcuteAsync()
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;
            IBusinessFlowHost host = services.GetService<IBusinessFlowHost>() ?? throw new BusinessFlowException("获取服务失败");
            User initiatorUser = _userCMX2;
            #region 执行自动流程
            //WriteTestInfo("执行未完成的自动节点....");
            //await host.RunAutoNodeAsync(_testFlowTemplate.ID);
            #endregion
            #region 启动一个流程
            {
                WriteTestInfo("启动一个新流程....");
                await host.StartNewFlowAsync(_testFlowTemplate.ID, initiatorUser.ID);
            }
            #endregion
            #region 保存数据
            {
                //WriteTestInfo("保存节点数据....");
                //List<FlowRecordDTO> flowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //if (flowRecords.Count <= 0) return;
                //string jsonData = "{\"Name\":\"测试数据\",\"Age\":0,\"Type\":\"病假\",\"StartDateTime\":\"2023-04-12 09:00:00\",\"EndDateTime\":\"2023-04-12 18:00:00\",\"Reason\":\"测试数据\"}";
                //await host.SaveFlowDataAsync(flowRecords[0].FlowTemplateID, flowRecords[0].ID, jsonData);
            }
            #endregion
            #region 完成节点
            {
                //string jsonData;
                //List<FlowRecordDTO> cmxflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //List<FlowRecordDTO> wrdflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userWRD.ID);
                //List<FlowRecordDTO> fbwflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userFBW.ID);
                //if (cmxflowRecords.Count > 0 && cmxflowRecords[0].StepID == _steps[0].ID)
                //{
                //    WriteTestInfo("----------------------------------");
                //    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                //    WriteTestInfo("回车执行流程:提交请假信息");
                //    Console.ReadKey();
                //    jsonData = $"{{\"Name\":\"{initiatorUser.Name}\",\"Age\":29,\"Type\":\"病假\",\"StartDateTime\":\"2023-04-12 09:00:00\",\"EndDateTime\":\"2023-04-12 18:00:00\",\"Reason\":\"生病了\"}}";
                //    await host.ComplateNodeAsync(cmxflowRecords[0].FlowTemplateID, cmxflowRecords[0].ID, initiatorUser.ID, jsonData);
                //    cmxflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //    wrdflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userWRD.ID);
                //    fbwflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userFBW.ID);
                //}
                //if (wrdflowRecords.Count > 0 && wrdflowRecords[0].StepID == _steps[1].ID)
                //{
                //    WriteTestInfo("----------------------------------");
                //    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                //    WriteTestInfo("回车执行流程:打回请假");
                //    Console.ReadKey();
                //    jsonData = "{\"Result1\":\"不同意\"}";
                //    await host.RepulseNodeAsync(wrdflowRecords[0].FlowTemplateID, wrdflowRecords[0].ID, _userWRD.ID, jsonData);//打回
                //    cmxflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //    wrdflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userWRD.ID);
                //    fbwflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userFBW.ID);
                //}
                //if (cmxflowRecords.Count > 0 && cmxflowRecords[0].StepID == _steps[0].ID)
                //{
                //    WriteTestInfo("----------------------------------");
                //    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                //    WriteTestInfo("回车执行流程:重新提交请假信息");
                //    Console.ReadKey();
                //    jsonData = $"{{\"Name\":\"{initiatorUser.Name}\",\"Age\":29,\"Type\":\"病假\",\"Reason\":\"生病了,再次提交\"}}";
                //    await host.ComplateNodeAsync(cmxflowRecords[0].FlowTemplateID, cmxflowRecords[0].ID, initiatorUser.ID, jsonData);
                //    cmxflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //    wrdflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userWRD.ID);
                //    fbwflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userFBW.ID);
                //}
                //if (wrdflowRecords.Count > 0 && wrdflowRecords[0].StepID == _steps[1].ID)
                //{
                //    WriteTestInfo("----------------------------------");
                //    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                //    WriteTestInfo("回车执行流程:同意请假");
                //    Console.ReadKey();
                //    jsonData = "{\"Result1\":\"同意\"}";
                //    await host.ComplateNodeAsync(wrdflowRecords[0].FlowTemplateID, wrdflowRecords[0].ID, _userWRD.ID, jsonData);
                //    cmxflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //    wrdflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userWRD.ID);
                //    fbwflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userFBW.ID);
                //}
                //if (fbwflowRecords.Count > 0 && fbwflowRecords[0].StepID == _steps[2].ID)
                //{
                //    WriteTestInfo("----------------------------------");
                //    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                //    WriteTestInfo("回车执行流程:上级同意请假");
                //    Console.ReadKey();
                //    jsonData = "{\"Result2\":\"同意\"}";
                //    await host.ComplateNodeAsync(fbwflowRecords[0].FlowTemplateID, fbwflowRecords[0].ID, _userFBW.ID, jsonData);
                //    cmxflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, initiatorUser.ID);
                //    wrdflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userWRD.ID);
                //    fbwflowRecords = await host.GetBacklogByUserIDAsync(_testFlowTemplate.ID, _userFBW.ID);
                //}
                //WriteTestInfo("----------------------------------");
                //WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
            }
            #endregion
            #region 修改数据模型
            {
                WriteTestInfo("新增字段....");
                DataModelField fieldResult3 = new() { Name = "Result3", Description = "审批结果3", DataType = DataTypeEnum.Enum, DataModelID = _testDataModel.ID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852694") };
                await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult3);
                _testDataModelFields.Add(fieldResult3);
                //修改字段
                WriteTestInfo("修改字段....");
                DataModelField fieldResult4 = new() { Name = "Result4", Description = "审批结果4", DataType = DataTypeEnum.Enum, DataModelID = _testDataModel.ID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852695") };
                await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult4);
                fieldResult4.Name = "Result5";
                fieldResult4.Description = "审批结果5";
                await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult4, true);
                //删除字段
                WriteTestInfo("删除字段....");
                await services.GetRequiredService<IDataModelFieldService>().DeleteAsync(fieldResult4.ID);
            }
            #endregion
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private static async Task InitAsync()
        {
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;
            WriteTestInfo("初始化用户信息....");
            await InitDomainAsync<User, IUserService>(services, _userCMX);
            await InitDomainAsync<User, IUserService>(services, _userCMX2);
            await InitDomainAsync<User, IUserService>(services, _userFBW);
            await InitDomainAsync<User, IUserService>(services, _userWRD);
            #region 数据模型
            WriteTestInfo("初始化数据模型....");
            await InitDomainAsync<DataModel, IDataModelService>(services, _testDataModel);
            DataModelField fieldName = new() { Name = "Name", Description = "姓名", DataType = DataTypeEnum.String, DataModelID = _testDataModel.ID, ID = Guid.Parse("48F31453-8D6C-4D53-9EA0-5C343FD37ADC") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldName);
            _testDataModelFields.Add(fieldName);
            DataModelField fieldAge = new() { Name = "Age", Description = "年龄", DataType = DataTypeEnum.Number, DataModelID = _testDataModel.ID, ID = Guid.Parse("4C6B8259-9E73-4D57-9573-08ECA17F5F1E") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldAge);
            _testDataModelFields.Add(fieldAge);
            DataModelField fieldType = new() { Name = "Type", Description = "请假类型", DataType = DataTypeEnum.Enum, DataModelID = _testDataModel.ID, Data = "[\"事假\",\"病假\",\"调休\"]", ID = Guid.Parse("5AF9F8B9-5664-40AB-B8B1-AFEDC40351DB") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldType);
            _testDataModelFields.Add(fieldType);
            DataModelField fieldStartDateTime = new() { Name = "StartDateTime", Description = "开始时间", DataType = DataTypeEnum.DateTime, DataModelID = _testDataModel.ID, ID = Guid.Parse("5AF9F8B9-5664-40AB-B8B1-AFEDC40351DC") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldStartDateTime);
            _testDataModelFields.Add(fieldStartDateTime);
            DataModelField fieldEndDateTime = new() { Name = "EndDateTime", Description = "结束时间", DataType = DataTypeEnum.DateTime, DataModelID = _testDataModel.ID, ID = Guid.Parse("5AF9F8B9-5664-40AB-B8B1-AFEDC40351DA") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldEndDateTime);
            _testDataModelFields.Add(fieldEndDateTime);
            DataModelField fieldReason = new() { Name = "Reason", Description = "请假原因", DataType = DataTypeEnum.String, DataModelID = _testDataModel.ID, ID = Guid.Parse("B5F4F349-6462-4CD2-90DA-88CA05F68BAA") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldReason);
            _testDataModelFields.Add(fieldReason);
            DataModelField fieldResult = new() { Name = "Result1", Description = "审批结果1", DataType = DataTypeEnum.Enum, DataModelID = _testDataModel.ID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("84F5A67E-4E09-456E-BAB8-0EC4D5D6D435") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult);
            _testDataModelFields.Add(fieldResult);
            DataModelField fieldResult2 = new() { Name = "Result2", Description = "审批结果2", DataType = DataTypeEnum.Enum, DataModelID = _testDataModel.ID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852693") };
            await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult2);
            _testDataModelFields.Add(fieldResult2);
            #endregion
            await InitDomainAsync<FlowTemplate, IFlowTemplateService>(services, _testFlowTemplate);
            #region 步骤
            WriteTestInfo("初始化流程模版....");
            #region 步骤0
            Step step0 = new()
            {
                ID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F50"),
                Name = "填写原因",
                FlowTemplateID = _testFlowTemplate.ID,
                NextID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F51"),
                UpID = null
            };
            await InitDomainAsync<Step, IStepService>(services, step0);
            _steps.Add(step0);
            _nodes.Add(step0.ID, new());
            #region 节点
            Node node00 = new()
            {
                ID = Guid.Parse("7446ab2d-33cf-414d-869c-eac487ffc600"),
                Name = "填写原因",
                Data = "",
                HandleType = NodeHandleTypeEnum.Initiator,
                HandleData = _userCMX.ID.ToString(),
                StepID = step0.ID
            };
            await InitDomainAsync<Node, INodeService>(services, node00);
            _nodes[step0.ID].Add(node00);
            #endregion
            #endregion
            #region 步骤1
            Step step1 = new()
            {
                ID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F51"),
                Name = "主管审批",
                FlowTemplateID = _testFlowTemplate.ID,
                NextID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F52"),
                UpID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F50"),
            };
            await InitDomainAsync<Step, IStepService>(services, step1);
            _steps.Add(step1);
            _nodes.Add(step1.ID, new());
            #region 节点
            Node node10 = new()
            {
                ID = Guid.Parse("7446ab2d-33cf-414d-869c-eac487ffc610"),
                Name = "主管审批",
                Data = "",
                HandleType = NodeHandleTypeEnum.User,
                HandleData = _userWRD.ID.ToString(),
                StepID = step1.ID
            };
            await InitDomainAsync<Node, INodeService>(services, node10);
            _nodes[step1.ID].Add(node10);
            Node node11 = new()
            {
                ID = Guid.Parse("7446ab2d-33cf-414d-869c-eac487ffc611"),
                Name = "输出节点",
                Data = "Reason",
                HandleType = NodeHandleTypeEnum.Auto,
                HandleData = "ConsoleMessageAutoNode",
                StepID = step1.ID
            };
            await InitDomainAsync<Node, INodeService>(services, node11);
            _nodes[step1.ID].Add(node11);
            #endregion
            #endregion
            #region 步骤2
            Step step2 = new()
            {
                ID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F52"),
                Name = "主管审批2",
                FlowTemplateID = _testFlowTemplate.ID,
                NextID = null,
                UpID = Guid.Parse("F5F8B1C1-1B9D-4F5A-9F5C-1B9D4F5A9F51"),
            };
            await InitDomainAsync<Step, IStepService>(services, step2);
            _steps.Add(step2);
            _nodes.Add(step2.ID, new());
            #region 节点
            Node node20 = new()
            {
                ID = Guid.Parse("7446ab2d-33cf-414d-869c-eac487ffc620"),
                Name = "主管审批2",
                Data = "",
                HandleType = NodeHandleTypeEnum.User,
                HandleData = _userFBW.ID.ToString(),
                StepID = step2.ID,
                RunConditionExpression = "{F|Result1}[Equal]{C|同意|String}"
            };
            await InitDomainAsync<Node, INodeService>(services, node20);
            _nodes[step2.ID].Add(node20);
            #endregion
            #endregion
            #endregion
        }
        /// <summary>
        /// 初始化Domain
        /// </summary>
        /// <typeparam name="TDomain"></typeparam>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <param name="domain"></param>
        /// <returns></returns>
        /// <exception cref="BusinessFlowException"></exception>
        private static async Task InitDomainAsync<TDomain, TService>(IServiceProvider services, TDomain domain, bool isEdit = false)
            where TDomain : class, IBaseDomain
            where TService : IBaseService<TDomain>
        {
            TService service = services.GetService<TService>() ?? throw new BusinessFlowException("获取服务失败");
            try
            {
                TDomain dbDomain = await service.GetInfoAsync(domain.ID);
                if(dbDomain != null && isEdit)
                {
                    await service.EditAsync(domain);
                }
            }
            catch
            {
                await service.AddAsync(domain);
            }
        }
        /// <summary>
        /// 写测试信息
        /// </summary>
        /// <param name="message"></param>
        private static void WriteTestInfo(string message)
        {
            //_logger?.LogInformation(message);
            Console.WriteLine($"{DateTime.Now:yyyy/MM/dd HH:mm:ss}->{message}");
        }
    }
}