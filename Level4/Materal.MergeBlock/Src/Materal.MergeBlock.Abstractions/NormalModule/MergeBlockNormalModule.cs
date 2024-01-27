
namespace Materal.MergeBlock.Abstractions.NormalModule
{
    /// <summary>
    /// MergeBlock普通模块
    /// </summary>
    public class MergeBlockNormalModule(string description, string? moduleName = null, string[]? depends = null) : MergeBlockModule(description, moduleName, depends), IMergeBlockNormalModule
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public MergeBlockNormalModule(string description, string[]? depends) : this(description, null, depends)
        {
        }
        /// <summary>
        /// 应用程序初始化之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitBeforeAsync(INormalApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAsync(INormalApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 应用程序初始化之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnApplicationInitAfterAsync(INormalApplicationContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceBeforeAsync(INormalConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAsync(INormalConfigServiceContext context) => await Task.CompletedTask;
        /// <summary>
        /// 配置服务之后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task OnConfigServiceAfterAsync(INormalConfigServiceContext context) => await Task.CompletedTask;
    }
}
