using Materal.APP.Core;
using Materal.APP.Core.ConfigModels;
using Materal.APP.WebAPICore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Authority.Server
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup : MateralAPPWebAPIStartup
    {
        /// <summary>
        /// ���췽��
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration) : base(configuration)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            WebAPIStartupConfig config = new WebAPIStartupConfig
            {
                AppName = "Authority",
                SwaggerXmlPathArray = new[]
                {
                    $"{basePath}/Authority.Server.xml",
                    //$"{basePath}/Authority.PresentationModel.xml",
                    //$"{basePath}/Authority.DataTransmitModel.xml"
                }
            };
            Init(config);
        }
        /// <summary>
        /// ���÷�������
        /// </summary>
        /// <param name="services"></param>
        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);
            ConfigureConsulServices(services, ServiceType.AuthorityServer);
            ConfigureOperationServices(services);
        }
        #region ˽�з���
        #region ҵ��
        /// <summary>
        /// ����ҵ�����
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureOperationServices(IServiceCollection services)
        {
        }
        #endregion
        #endregion
    }
}
