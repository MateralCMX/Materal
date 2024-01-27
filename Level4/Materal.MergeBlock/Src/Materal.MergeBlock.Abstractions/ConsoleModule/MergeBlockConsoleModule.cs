namespace Materal.MergeBlock.Abstractions.ConsoleModule
{
    /// <summary>
    /// MergeBlock控制台模块
    /// </summary>
    public abstract class MergeBlockConsoleModule(string description, string? moduleName = null, string[]? depends = null) : MergeBlockModule(description, moduleName, depends), IMergeBlockConsoleModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockConsoleModule(string description, string[]? depends) : this(description, null, depends)
        {
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitBeforeAsync(IConsoleApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAsync(IConsoleApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAfterAsync(IConsoleApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceBeforeAsync(IConsoleConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAsync(IConsoleConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAfterAsync(IConsoleConfigServiceContext context) => await Task.CompletedTask;
    }
}
