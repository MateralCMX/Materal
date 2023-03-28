using Materal.Logger;
using Materal.Workflow.StepBodys;
using Materal.Workflow.StepDatas;
using Materal.Workflow.WorkflowCoreExtension;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Reflection;

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
            const string runtimJson = "{\"RuntimeMessage\":\"动态工作流运行[RuntimeData]\"}";
            Dictionary<string, object?> runtimeData = runtimJson.JsonToObject<Dictionary<string, object?>>();
            string workflowDataJsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "WorkflowData.json");
            string workflowDataJson = File.ReadAllText(workflowDataJsonFilePath);
            StartStepData stepData = workflowDataJson.JsonToStartStepData();
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