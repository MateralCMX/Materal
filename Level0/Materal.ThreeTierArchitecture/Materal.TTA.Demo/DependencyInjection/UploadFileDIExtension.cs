using Microsoft.Extensions.DependencyInjection;

namespace DependencyInjection
{
    /// <summary>
    /// 上传文件依赖注入扩展类
    /// </summary>
    public static class UploadFileDIExtension
    {
        /// <summary>
        /// 添加上传文件服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddUploadFileServices(this IServiceCollection services)
        {
            //services.AddDbContextPool<UploadFileDbContext>(options => options.UseSqlServer(ApplicationConfig.UploadFileDB.ConnectionString, m =>
            //{
            //    m.EnableRetryOnFailure();
            //}));
            //services.AddTransient(typeof(IUploadFileUnitOfWork), typeof(UploadFileUnitOfWorkImpl));
            //services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("UploadFile.EFRepository"))
            //    .Where(c => c.Name.EndsWith("RepositoryImpl"))
            //    .AsPublicImplementedInterfaces();
            //services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("UploadFile.ServiceImpl"))
            //    .Where(c => c.Name.EndsWith("ServiceImpl"))
            //    .AsPublicImplementedInterfaces();
        }
    }
}
