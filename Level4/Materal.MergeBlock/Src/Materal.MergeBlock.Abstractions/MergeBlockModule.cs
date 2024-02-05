
namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块
    /// </summary>
    public class MergeBlockModule : IMergeBlockModule
    {
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description { get; }
        /// <summary>
        /// 模块
        /// </summary>
        public string ModuleName { get; }
        /// <summary>
        /// 依赖
        /// </summary>
        public string[] Depends { get; }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockModule(string description, string? moduleName = null, string[]? depends = null)
        {
            Description = description;
            ModuleName = moduleName ?? GetType().Name;
            Depends = depends ?? [];
        }
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockModule(string description, string[]? depends) : this(description, null, depends)
        {
        }
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAfterAsync(IApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAsync(IApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitBeforeAsync(IApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAfterAsync(IConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAsync(IConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceBeforeAsync(IConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序启动之后
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationStartdAsync(IServiceProvider serviceProvider) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序结束之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationCloseBeforeAsync(IApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序结束
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationCloseAsync(IApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序结束之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationCloseAfterAsync(IApplicationContext context) => await Task.CompletedTask;
    }
}
