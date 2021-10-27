using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.HttpManage;
using Materal.APP.Core;
using Materal.APP.DataTransmitModel;
using Materal.APP.HttpManage;
using Materal.Common;
using Materal.Model;
using Materal.NetworkHelper;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
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
        /// ����WebHost
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
        /// ��ʼ��Http������
        /// </summary>
        /// <returns></returns>
        private static async Task InitHttpManage()
        {
            if(HttpManager.HeaderHandler == null) throw new WebAPPException("δ�ҵ�HeaderHandler������");
            var serverManage = ApplicationData.GetService<IServerManage>();
            if(serverManage == null) throw new WebAPPException($"δ�ҵ�{nameof(IServerManage)}����");
            try
            {
                ResultModel<List<ServerListDTO>> resultModel = await serverManage.GetServerListAsync();
                if (resultModel.ResultType == ResultTypeEnum.Success)
                {
                    UrlManage.Init(resultModel.Data);
                    if (string.IsNullOrWhiteSpace(UrlManage.ConfigCenterUrl)) return;
                    var configCenterManage = ApplicationData.GetService<IConfigCenterManage>();
                    ResultModel<List<EnvironmentListDTO>> environmentListResultModel = await configCenterManage.GetEnvironmentListAsync();
                    if (environmentListResultModel.ResultType == ResultTypeEnum.Success)
                    {
                        UrlManage.InitEnvironmentUrl(environmentListResultModel.Data);
                    }
                    else
                    {
                        WebAPPConsoleHelper.WriteLine(resultModel.Message);
                    }
                }
                else
                {
                    WebAPPConsoleHelper.WriteLine(resultModel.Message);
                }
            }
            catch (Exception exception)
            {
                throw new WebAPPException("����������ʧ��", exception);
            }
        }
    }
}