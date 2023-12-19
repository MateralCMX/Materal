using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock服务
    /// </summary>
    public class MergeBlockService(IConfiguration configuration, IWebHostEnvironment environment, ILogger<MergeBlockService>? logger) : IMergeBlockService
    {
        /// <summary>
        /// 初始化MergeBlock管理器
        /// </summary>
        public void InitMergeBlockManage()
        {
            string? urlsValue = configuration.GetValue("URLS");
            if (environment.IsDevelopment() && string.IsNullOrWhiteSpace(urlsValue))
            {
                urlsValue ??= configuration.GetValue("ASPNETCORE_URLS");
            }
            urlsValue ??= "http://localhost:5000";
            string[] urls = urlsValue.Split(";");
            foreach (string url in urls)
            {
                MergeBlockManager.BaseUris.Add(new(url));
            }
            logger?.LogDebug("MergeBlockManager初始化完毕");
        }
    }
}
