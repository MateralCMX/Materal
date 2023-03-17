using Materal.Logger;
using Materal.Workflow.StepDatas;
using Materal.Workflow.Steps;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.Workflow.Demo
{
    public class Program
    {
        public static void Main()
        {
            #region 初始化
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddDynamicWorkflowService();
            serviceCollection.AddMateralLogger();
            IServiceProvider service = serviceCollection.BuildServiceProvider();
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole", "${DateTime}|${Level}|${CategoryName}:${Message}\r\n${Exception}", new()
                {
                    [LogLevel.Debug] = ConsoleColor.DarkGreen,
                    [LogLevel.Warning] = ConsoleColor.DarkYellow,
                    [LogLevel.Error] = ConsoleColor.DarkRed
                });
                option.AddAllTargetRule(LogLevel.Warning);
            }, null);
            IDynamicWorkflowHost? host = service.GetService<IDynamicWorkflowHost>() ?? throw new Exception("获取服务失败");
            host.Start();
            #endregion
            #region 流程定义
            const string buildDataJson = "{\"Name\":\"Materal\",\"Number\":1,\"Message\":\"个人介绍\"}";
            Dictionary<string, object?> buildData = buildDataJson.JsonToObject<Dictionary<string, object?>>();
            const string runtimJson = "{\"Name\":\"Materal\",\"Number\":1.2,\"Message\":\"动态工作流运行[RuntimeData]\",\"Result\":\"\"}";
            Dictionary<string, object?> runtimeData = runtimJson.JsonToObject<Dictionary<string, object?>>();
            StartStepData stepData = new()
            {
                Next = new IfStepData("Name", ValueSourceEnum.RuntimeDataProperty, ComparisonTypeEnum.Equal, "Message", ValueSourceEnum.RuntimeDataProperty)
                {
                    BuildData = buildData,
                    StepData = new ThenStepData<ConsoleMessageStep>
                    {
                        Inputs = new()
                        {
                            new InputData(nameof(ConsoleMessageStep.Message), "工作流运行[0]")
                        }
                    },
                    Next = new ThenStepData<ConsoleMessageStep>
                    {
                        Inputs = new()
                        {
                            new InputData(nameof(ConsoleMessageStep.Message), "工作流运行[1]")
                        }
                    }
                }
            };
            #endregion
            #region 工作流启动
            host.StartDynamicWorkflow(stepData, runtimeData);
            //Console.WriteLine("按任意键发送事件MyEvent->Materal");
            //Console.ReadKey();
            //host.PublishEvent("MyEvent", "Materal", "MyEvent->Materal的Message");
            //host.PublishEvent("MyEvent", "Materal", new { Message = "MyEvent->Materal的Message" });
            //PendingActivity? activity = host.GetPendingActivity("MyActivity", "1111111Materal", TimeSpan.FromSeconds(10)).Result;
            //if (activity != null)
            //{
            //    string? message = activity.Parameters.ToString();
            //    host.SubmitActivitySuccess(activity.Token, new { Message = $"MyActivity的Message:{message}" });
            //}
            #endregion
            Console.WriteLine("按任意键退出");
            Console.ReadKey();
            host.Stop();
        }
    }
}