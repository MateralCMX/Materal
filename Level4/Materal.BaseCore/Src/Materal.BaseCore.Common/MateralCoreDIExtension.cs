namespace Materal.BaseCore.Common
{
    /// <summary>
    /// 发布中心依赖注入扩展
    /// </summary>
    public static class MateralCoreDIExtension
    {
        /// <summary>
        /// 添加发布中心服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="autoMapperAssemblys"></param>
        /// <returns></returns>
        public static IServiceCollection AddMateralCoreServices(this IServiceCollection services, params Assembly[] autoMapperAssemblys)
        {
            PageRequestModel.PageStartNumber = 1;
            services.AddMemoryCache();
            services.AddMateralUtils();
            List<Assembly> autoMapperAssemblyList = autoMapperAssemblys.ToList();
            autoMapperAssemblyList.Add(Assembly.Load("Materal.BaseCore.WebAPI"));
            services.AddAutoMapper(config =>
            {
                config.AllowNullCollections = true;
            }, autoMapperAssemblyList);
            return services;
        }
    }
}
