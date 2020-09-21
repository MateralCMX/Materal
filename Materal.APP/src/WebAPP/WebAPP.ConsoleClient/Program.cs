using System;
using Materal.APP.Core;
using Materal.APP.DataTransmitModel;
using Materal.APP.HttpManage;
using Materal.Common;
using Materal.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
using ConfigCenter.DataTransmitModel.ConfigCenter;
using ConfigCenter.HttpManage;
using Materal.NetworkHelper;
using WebAPP.Common;
using WebAPP.HttpClientImpl;

namespace WebAPP.ConsoleClient
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddWebAPPServices();
            ApplicationData.ServiceProvider = services.BuildServiceProvider();
            await InitHttpManage();
        }
        /// <summary>
        /// 初始化Http管理器
        /// </summary>
        /// <returns></returns>
        private static async Task InitHttpManage()
        {
            if (HttpManager.HeaderHandler == null) throw new WebAPPException("未找到HeaderHandler处理器");
            var serverManage = ApplicationData.GetService<IServerManage>();
            if (serverManage == null) throw new WebAPPException($"未找到{nameof(IServerManage)}服务");
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
                throw new WebAPPException("连接主服务失败", exception);
            }
        }
    }
}
