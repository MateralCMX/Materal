using Materal.Abstractions;
using Materal.BusinessFlow.Abstractions;
using Materal.BusinessFlow.Abstractions.Domain;
using Materal.BusinessFlow.Abstractions.DTO;
using Materal.BusinessFlow.Abstractions.Services;
using Materal.BusinessFlow.Abstractions.Services.Models;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModel;
using Materal.BusinessFlow.Abstractions.Services.Models.DataModelField;
using Materal.BusinessFlow.Abstractions.Services.Models.FlowTemplate;
using Materal.BusinessFlow.Abstractions.Services.Models.Node;
using Materal.BusinessFlow.Abstractions.Services.Models.Step;
using Materal.BusinessFlow.Abstractions.Services.Models.User;
using Materal.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

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
        private static readonly ILogger<Program>? _logger;
        private static Guid _flowTemplateID = Guid.NewGuid();
        private static readonly List<Guid> _stepIDs = new()
        {
            Guid.Parse("250CF199-E3EB-47DD-B4E8-91284D37AE7F"),
            Guid.Parse("4202744A-B6B9-41AF-A323-257A250B085F"),
            Guid.Parse("62EBF9E1-C9B9-4169-9310-8B9CB4D420E7")
        };
        static Program()
        {
            MateralConfig.PageStartNumber = 1;
            IServiceCollection services = new ServiceCollection();
            services.AddMateralLogger();
            services.AddBusinessFlow();
            IRepositoryHelper repositoryHelper = new SqliteRepositoryHelper();
            //IRepositoryHelper repositoryHelper = new SqlServerRepositoryHelper();
            repositoryHelper.AddRepository(services);
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
            repositoryHelper.Init(_serviceProvider);
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
            User initiatorUser = _userCMX;
            Guid flowID = Guid.Parse("161A38C6-8C02-4A28-9A52-D22CB10FE636");
            #region 执行自动流程
            WriteTestInfo("执行未完成的自动节点....");
            await host.RunAutoNodeAsync(_flowTemplateID);
            #endregion
            #region 启动一个流程
            {
                //WriteTestInfo("启动一个新流程....");
                flowID = await host.StartNewFlowAsync(_flowTemplateID, initiatorUser.ID);
            }
            #endregion
            #region 保存数据
            {
                //WriteTestInfo("保存节点数据....");
                //List<FlowRecordDTO> flowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                //if (flowRecords.Count <= 0) return;
                //string jsonData = "{\"Name\":\"测试数据\",\"Age\":0,\"Type\":\"病假\",\"StartDateTime\":\"2023-04-12 09:00:00\",\"EndDateTime\":\"2023-04-12 18:00:00\",\"Reason\":\"测试数据\"}";
                //await host.SaveFlowDataAsync(flowRecords[0].FlowTemplateID, flowRecords[0].ID, jsonData);
            }
            #endregion
            #region 完成节点
            {
                //List<FlowRecordDTO> flowRecords = await host.GetBacklogByUserIDAsync(initiatorUser.ID);
                //List<FlowTemplate> flowTemplates = await host.GetBacklogFlowTemplatesByUserIDAsync(initiatorUser.ID);
                string jsonData;
                List<FlowRecordDTO> cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                List<FlowRecordDTO> wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                List<FlowRecordDTO> fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                FlowRecordDTO? flowRecord;
                flowRecord = cmxflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[0]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:提交请假信息");
                    Console.ReadKey();
                    jsonData = $"{{\"Name\":\"{initiatorUser.Name}\",\"Age\":29,\"Type\":\"病假\",\"StartDateTime\":\"2023-04-12 09:00:00\",\"EndDateTime\":\"2023-04-12 18:00:00\",\"Reason\":\"生病了\"}}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = wrdflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[1]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:打回请假");
                    Console.ReadKey();
                    jsonData = "{\"Result1\":\"不同意\"}";
                    await host.RepulseFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);//打回
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = cmxflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[0]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:重新提交请假信息");
                    Console.ReadKey();
                    jsonData = $"{{\"Name\":\"{initiatorUser.Name}\",\"Age\":29,\"Type\":\"病假\",\"Reason\":\"生病了,再次提交\"}}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = wrdflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[1]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:同意请假");
                    Console.ReadKey();
                    jsonData = "{\"Result1\":\"同意\"}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                flowRecord = fbwflowRecords.FirstOrDefault(m => m.FlowID == flowID && m.StepID == _stepIDs[2]);
                if (flowRecord != null)
                {
                    WriteTestInfo("----------------------------------");
                    WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
                    WriteTestInfo("回车执行流程:上级同意请假");
                    Console.ReadKey();
                    jsonData = "{\"Result2\":\"同意\"}";
                    await host.ComplateFlowNodeAsync(flowRecord.FlowTemplateID, flowRecord.ID, initiatorUser.ID, jsonData);
                    cmxflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, initiatorUser.ID);
                    wrdflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userWRD.ID);
                    fbwflowRecords = await host.GetBacklogByUserIDAsync(_flowTemplateID, _userFBW.ID);
                }
                WriteTestInfo("----------------------------------");
                WriteTestInfo($"待办事项：{initiatorUser.Name}->{cmxflowRecords.Count},{_userWRD.Name}->{wrdflowRecords.Count},{_userFBW.Name}->{fbwflowRecords.Count}");
            }
            #endregion
            #region 修改数据模型
            {
                //WriteTestInfo("新增字段....");
                //DataModelField fieldResult3 = new() { Name = "Result3", Description = "审批结果3", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852694") };
                //await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult3);
                //_testDataModelFields.Add(fieldResult3);
                ////修改字段
                //WriteTestInfo("修改字段....");
                //DataModelField fieldResult4 = new() { Name = "Result4", Description = "审批结果4", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"同意\",\"不同意\"]", ID = Guid.Parse("62BBEF3F-89FB-47ED-B875-04F74C852695") };
                //await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult4);
                //fieldResult4.Name = "Result5";
                //fieldResult4.Description = "审批结果5";
                //await InitDomainAsync<DataModelField, IDataModelFieldService>(services, fieldResult4, true);
                ////删除字段
                //WriteTestInfo("删除字段....");
                //await services.GetRequiredService<IDataModelFieldService>().DeleteAsync(fieldResult4.ID);
            }
            #endregion
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        private static async Task InitAsync()
        {
            bool isAdd;
            using IServiceScope serviceScope = _serviceProvider.CreateScope();
            IServiceProvider services = serviceScope.ServiceProvider;
            WriteTestInfo("初始化用户信息....");
            await InitDomainAsync<User, User, AddUserModel, EditUserModel, IUserService>(services, _userCMX);
            await InitDomainAsync<User, User, AddUserModel, EditUserModel, IUserService>(services, _userCMX2);
            await InitDomainAsync<User, User, AddUserModel, EditUserModel, IUserService>(services, _userFBW);
            await InitDomainAsync<User, User, AddUserModel, EditUserModel, IUserService>(services, _userWRD);
            #region 数据模型
            WriteTestInfo("初始化数据模型....");
            (Guid dataModelID, isAdd) = await InitDomainAsync<DataModel, DataModel, AddDataModelModel, EditDataModelModel, IDataModelService>(services, new() { ID = Guid.Parse("1A731CA3-43AA-4F93-A409-B60DACA58DAA"), Name = "测试数据模型" });
            if (isAdd)
            {
                DataModelField fieldName = new() { Name = "Name", Description = "姓名", DataType = DataTypeEnum.String, DataModelID = dataModelID };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldName);
                DataModelField fieldAge = new() { Name = "Age", Description = "年龄", DataType = DataTypeEnum.Number, DataModelID = dataModelID };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldAge);
                DataModelField fieldType = new() { Name = "Type", Description = "请假类型", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"事假\",\"病假\",\"调休\"]" };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldType);
                DataModelField fieldStartDateTime = new() { Name = "StartDateTime", Description = "开始时间", DataType = DataTypeEnum.DateTime, DataModelID = dataModelID };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldStartDateTime);
                DataModelField fieldEndDateTime = new() { Name = "EndDateTime", Description = "结束时间", DataType = DataTypeEnum.DateTime, DataModelID = dataModelID };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldEndDateTime);
                DataModelField fieldReason = new() { Name = "Reason", Description = "请假原因", DataType = DataTypeEnum.String, DataModelID = dataModelID };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldReason);
                DataModelField fieldResult = new() { Name = "Result1", Description = "审批结果1", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"同意\",\"不同意\"]" };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldResult);
                DataModelField fieldResult2 = new() { Name = "Result2", Description = "审批结果2", DataType = DataTypeEnum.Enum, DataModelID = dataModelID, Data = "[\"同意\",\"不同意\"]" };
                await InitDomainAsync<DataModelField, DataModelFieldDTO, AddDataModelFieldModel, EditDataModelFieldModel, IDataModelFieldService>(services, fieldResult2);
            }
            #endregion
            WriteTestInfo("初始化流程模版....");
            (_flowTemplateID, isAdd) = await InitDomainAsync<FlowTemplate, FlowTemplateDTO, AddFlowTemplateModel, EditFlowTemplateModel, IFlowTemplateService>(services, new()
            {
                ID = Guid.Parse("1542FEAA-C5DA-4FD2-B05A-4705730EFED1"),
                Name = "请假流程模版",
                DataModelID = dataModelID
            });
            #region 步骤
            if (isAdd)
            {
                #region 步骤0
                Step step0 = new()
                {
                    ID = _stepIDs[0],
                    Name = "填写原因",
                    FlowTemplateID = _flowTemplateID,
                    UpID = null
                };
                #region 节点
                Node node00 = new()
                {
                    Name = "填写原因",
                    Data = "",
                    HandleType = NodeHandleTypeEnum.Initiator,
                    HandleData = _userCMX.ID.ToString(),
                    StepID = step0.ID
                };
                #endregion
                #endregion
                #region 步骤1
                Step step1 = new()
                {
                    ID = _stepIDs[1],
                    Name = "主管审批",
                    FlowTemplateID = _flowTemplateID,
                    UpID = step0.ID,
                };
                #region 节点
                Node node10 = new()
                {
                    Name = "主管审批",
                    Data = "",
                    HandleType = NodeHandleTypeEnum.User,
                    HandleData = _userWRD.ID.ToString(),
                    StepID = step1.ID
                };
                Node node11 = new()
                {
                    Name = "输出节点",
                    Data = "Reason",
                    HandleType = NodeHandleTypeEnum.Auto,
                    HandleData = "ConsoleMessageAutoNode",
                    StepID = step1.ID
                };
                #endregion
                #endregion
                #region 步骤2
                Step step2 = new()
                {
                    ID = _stepIDs[2],
                    Name = "主管审批2",
                    FlowTemplateID = _flowTemplateID,
                    UpID = step1.ID,
                };
                #region 节点
                Node node20 = new()
                {
                    Name = "主管审批2",
                    Data = "",
                    HandleType = NodeHandleTypeEnum.User,
                    HandleData = _userFBW.ID.ToString(),
                    StepID = step2.ID,
                    RunConditionExpression = "{F|Result1}[Equal]{C|同意|String}"
                };
                #endregion
                #endregion
                step0.UpID = null;
                step0.NextID = step1.ID;
                (Guid stepID, isAdd) = await InitDomainAsync<Step, Step, AddStepModel, EditStepModel, IStepService>(services, step0);
                _stepIDs.Add(stepID);
                await InitDomainAsync<Node, Node, AddNodeModel, EditNodeModel, INodeService>(services, node00);
                step1.UpID = step0.ID;
                step1.NextID = step2.ID;
                (stepID, isAdd) = await InitDomainAsync<Step, Step, AddStepModel, EditStepModel, IStepService>(services, step1);
                _stepIDs.Add(stepID);
                await InitDomainAsync<Node, Node, AddNodeModel, EditNodeModel, INodeService>(services, node10);
                await InitDomainAsync<Node, Node, AddNodeModel, EditNodeModel, INodeService>(services, node11);
                step2.UpID = step1.ID;
                step2.NextID = null;
                (stepID, isAdd) = await InitDomainAsync<Step, Step, AddStepModel, EditStepModel, IStepService>(services, step2);
                _stepIDs.Add(stepID);
                await InitDomainAsync<Node, Node, AddNodeModel, EditNodeModel, INodeService>(services, node20);
            }
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
        private static async Task<(Guid id, bool isAdd)> InitDomainAsync<TDomain, TDTO, TAddModel, TEditModel, TService>(IServiceProvider services, TDomain domain, bool isEdit = false)
            where TDomain : class, IDomain
            where TDTO : class, IDTO
            where TAddModel : class, new()
            where TEditModel : class, IEditModel, new()
            where TService : IBaseService<TDomain, TDTO, TAddModel, TEditModel>
        {
            TService service = services.GetService<TService>() ?? throw new BusinessFlowException("获取服务失败");
            try
            {
                TDTO dbDomain = await service.GetInfoAsync(domain.ID);
                if (isEdit)
                {
                    TEditModel editModel = domain.CopyProperties<TEditModel>();
                    await service.EditAsync(editModel);
                }
                return (dbDomain.ID, false);
            }
            catch
            {
                TAddModel addModel = domain.CopyProperties<TAddModel>();
                return (await service.AddAsync(addModel), true);
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