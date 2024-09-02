namespace Materal.MergeBlock.Abstractions
{
    /// <summary>
    /// MergeBlock模块
    /// </summary>
    public interface IMergeBlockModule
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        string Name { get; }
        /// <summary>
        /// 应用初始化前
        /// </summary>
        /// <param name="context"></param>
        void OnPreApplicationInitialization(ApplicationInitializationContext context);
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        void OnApplicationInitialization(ApplicationInitializationContext context);
        /// <summary>
        /// 应用初始化后
        /// </summary>
        /// <param name="context"></param>
        void OnPostApplicationInitialization(ApplicationInitializationContext context);
        /// <summary>
        /// 应用初始化前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnPreApplicationInitializationAsync(ApplicationInitializationContext context);
        /// <summary>
        /// 应用初始化
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnApplicationInitializationAsync(ApplicationInitializationContext context);
        /// <summary>
        /// 应用初始化后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnPostApplicationInitializationAsync(ApplicationInitializationContext context);
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        void OnPreConfigureServices(ServiceConfigurationContext context);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        void OnConfigureServices(ServiceConfigurationContext context);
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        void OnPostConfigureServices(ServiceConfigurationContext context);
        /// <summary>
        /// 配置服务前
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnPreConfigureServicesAsync(ServiceConfigurationContext context);
        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnConfigureServicesAsync(ServiceConfigurationContext context);
        /// <summary>
        /// 配置服务后
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        Task OnPostConfigureServicesAsync(ServiceConfigurationContext context);
        /// <summary>
        /// 判断当前类型是否为<see cref="T:Dy.MergeBlock.Abstractions.Core.IMergeBlockModule" />
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsMergeBlockModule(Type type)
            => !(type == null) && type.IsClass && !type.IsAbstract && !type.IsGenericType && typeof(IMergeBlockModule).IsAssignableFrom(type);
    }
}
