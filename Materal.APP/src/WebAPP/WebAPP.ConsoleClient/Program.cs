using Materal.APP.Core;
using Materal.APP.DataTransmitModel;
using Materal.APP.HttpManage;
using Materal.Common;
using Materal.Model;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Threading.Tasks;
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
            var serverManage = ApplicationData.GetService<IServerManage>();
            ResultModel<List<ServerListDTO>> resultModel = await serverManage.GetServerListAsync();
            if (resultModel?.ResultType == ResultTypeEnum.Success)
            {
                UrlManage.Init(resultModel.Data);
            }
        }
    }
}
