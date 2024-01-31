namespace Materal.MergeBlock
{
    /// <summary>
    /// 模块信息
    /// </summary>
    public abstract class ModuleInfo<TModule> : IModuleInfo<TModule>
        where TModule : IMergeBlockModule
    {
        /// <summary>
        /// 唯一标识
        /// </summary>
        public Guid ID { get; } = Guid.NewGuid();
        /// <summary>
        /// 模块类型
        /// </summary>
        public Type ModuleType { get; }
        /// <summary>
        /// 模块实例
        /// </summary>
        public TModule Instance { get; }
        /// <summary>
        /// 模块文件夹信息
        /// </summary>
        public IModuleDirectoryInfo ModuleDirectoryInfo { get; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name => Instance.ModuleName;
        /// <summary>
        /// 模块描述
        /// </summary>
        public string Description => Instance.Description;
        /// <summary>
        /// 依赖模块
        /// </summary>
        public string[] Depends => Instance.Depends;
        /// <summary>
        /// 模块位置
        /// </summary>
        public string Location => ModuleType.Assembly.Location;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="moduleDirectoryInfo"></param>
        /// <param name="moduleType"></param>
        public ModuleInfo(IModuleDirectoryInfo moduleDirectoryInfo, Type moduleType)
        {
            ModuleDirectoryInfo = moduleDirectoryInfo;
            ModuleType = moduleType;
            if (!ModuleType.IsAssignableTo<TModule>()) throw new MergeBlockException($"{moduleType.Name}未实现{nameof(TModule)}");
            Instance = ModuleType.Instantiation<TModule>();
        }
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ConfigServiceBeforeAsync(IConfigServiceContext context) => await Instance.OnConfigServiceBeforeAsync(context);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ConfigServiceAsync(IConfigServiceContext context) => await Instance.OnConfigServiceAsync(context);
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ConfigServiceAfterAsync(IConfigServiceContext context) => await Instance.OnConfigServiceAfterAsync(context);
        /// <summary>
        /// 应用程序初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ApplicationInitBeforeAsync(IApplicationContext context) => await Instance.OnApplicationInitBeforeAsync(context);
        /// <summary>
        /// 应用程序初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ApplicationInitAsync(IApplicationContext context) => await Instance.OnApplicationInitAsync(context);
        /// <summary>
        /// 应用程序初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ApplicationInitAfterAsync(IApplicationContext context) => await Instance.OnApplicationInitAfterAsync(context);
        /// <summary>
        /// 应用程序关闭前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ApplicationCloseBeforeAsync(IApplicationContext context) => await Instance.OnApplicationCloseBeforeAsync(context);
        /// <summary>
        /// 应用程序关闭
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ApplicationCloseAsync(IApplicationContext context) => await Instance.OnApplicationCloseAsync(context);
        /// <summary>
        /// 应用程序关闭后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public virtual async Task ApplicationCloseAfterAsync(IApplicationContext context) => await Instance.OnApplicationCloseAfterAsync(context);
    }
    /// <summary>
    /// 模块信息
    /// </summary>
    /// <param name="moduleDirectoryInfo"></param>
    /// <param name="moduleType"></param>
    public class ModuleInfo(IModuleDirectoryInfo moduleDirectoryInfo, Type moduleType) : ModuleInfo<MergeBlockModule>(moduleDirectoryInfo, moduleType)
    {
    }
}
