using Materal.APP.Core;
using Materal.APP.DataTransmitModel;
using Materal.APP.HttpManage;
using Materal.Common;
using Materal.Model;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPP.Common;
using WebAPP.HttpClientImpl;

namespace WebAPP
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            WebAssemblyHost webAssemblyHost = CreateWebHost(args);
            ApplicationData.ServiceProvider = webAssemblyHost.Services;
            await InitHttpManage();
            ConsoleHelperBase.StartWrite();
            await webAssemblyHost.RunAsync();
        }
        /// <summary>
        /// 创建WebHost
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        private static WebAssemblyHost CreateWebHost(string[] args)
        {
            var webAssemblyHostBuilder = WebAssemblyHostBuilder.CreateDefault(args);
            WebAPPConfig.SetConfiguration(webAssemblyHostBuilder.Configuration);
            webAssemblyHostBuilder.RootComponents.Add<App>("app");
            webAssemblyHostBuilder.Services.AddWebAPPServices();
            WebAssemblyHost webAssemblyHost = webAssemblyHostBuilder.Build();
            return webAssemblyHost;
        }
        /// <summary>
        /// 初始化Http管理器
        /// </summary>
        /// <returns></returns>
        private static async Task InitHttpManage()
        {
            var serverManage = ApplicationData.GetService<IServerManage>();
            ResultModel<List<ServerListDTO>> resultModel = await serverManage.GetServerListAsync();
            if (resultModel.ResultType == ResultTypeEnum.Success)
            {
                UrlManage.Init(resultModel.Data);
            }
            else
            {
                WebAPPConsoleHelper.WriteLine(resultModel.Message);
            }
        }
    }
}