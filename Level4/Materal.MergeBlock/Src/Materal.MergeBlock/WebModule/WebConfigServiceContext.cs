using Materal.MergeBlock.Abstractions.WebModule;
using Materal.MergeBlock.WebModule.Filters;
using Microsoft.AspNetCore.Builder;
using Newtonsoft.Json.Serialization;

namespace Materal.MergeBlock.WebModule
{
    /// <summary>
    /// Web配置服务上下文
    /// </summary>
    public class WebConfigServiceContext : ConfigServiceContext, IWebConfigServiceContext
    {
        /// <summary>
        /// 主机构建器
        /// </summary>
        public WebApplicationBuilder HostBuilder { get; }
        /// <summary>
        /// MVC构建器
        /// </summary>
        public IMvcBuilder MvcBuilder { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="hostBuilder"></param>
        public WebConfigServiceContext(WebApplicationBuilder hostBuilder) : base(hostBuilder.Services, hostBuilder.Configuration)
        {
            HostBuilder = hostBuilder;
            MvcBuilder = Services.AddControllers(options =>
            {
                options.Filters.Add<ActionPageQueryFilterAttribute>();
                options.Filters.Add<InitServiceFilterAttribute>();
                options.SuppressAsyncSuffixInActionNames = true;
                options.Conventions.Add(new MergeBlockControllerModelConvention());
            }).AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null)
            .AddNewtonsoftJson(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());
        }
    }
}
