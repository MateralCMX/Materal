using Demo.Common;
using Demo.Domain;
using Demo.Domain.Repositories;
using Demo.EFRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using System;
using System.Data.SqlClient;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Materal.Common;

namespace Demo.DependencyInjection
{
    public static class DemoDIExtension
    {
        /// <summary>
        /// 添加权限服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddDemoServices(this IServiceCollection services)
        {
            services.AddSingleton(DemoConfig.DemoDBConfig.SubordinateConfigs);
            services.AddSingleton<Action<DbContextOptionsBuilder, string>>((options, configString) => options.UseSqlServer(configString, m =>
            {
                m.EnableRetryOnFailure();
            }));
            services.AddDbContext<DemoDbContext>(options => options.UseSqlServer(DemoConfig.DemoDBConfig.ConnectionString, m =>
            {
                m.EnableRetryOnFailure();
            }));
            services.AddTransient(typeof(IDemoUnitOfWork), typeof(DemoUnitOfWorkImpl));
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Demo.EFRepository"))
                .Where(c => c.Name.EndsWith("RepositoryImpl"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(Assembly.Load("Demo.ServiceImpl"))
                .Where(c => c.Name.EndsWith("ServiceImpl"))
                .AsPublicImplementedInterfaces();

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            try
            {
                //var unitOfWork = serviceProvider.GetService<IDemoUnitOfWork>();
                //var repository = serviceProvider.GetService<IClassRepository>();
                //var @class = new Class
                //{
                //    Name = "236班",
                //    Remark = "初中一年级"
                //};
                //unitOfWork.RegisterAdd(@class);
                //unitOfWork.Commit();
                //Thread.Sleep(10000);
                //Class result = repository.FirstOrDefaultFromSubordinate(@class.ID);
                MateralConfig.PageStartNumber = 1;
                var repository = serviceProvider.GetService<IClassRepository>();
                Guid guid = Guid.Parse("076EEC47-9D1E-444C-9B57-08D71A78E4C3");
                //Task task = Task.Run(async () =>
                //{
                //    var result1 = await repository.FirstOrDefaultFromSubordinateAsync(guid);
                //    var result2 = await repository.CountFromSubordinateAsync(m => m.Name.Contains("23"));
                //    var result3 = await repository.ExistedFromSubordinateAsync(guid);
                //    var result4 = await repository.FindFromSubordinateAsync(m => m.Name.Contains("23"));
                //    var result5 = await repository.PagingFromSubordinateAsync(m => m.Name.Contains("23"), m => m.CreateTime, SortOrder.Ascending, 1, 10);
                //});
                //Task.WaitAll(task);
                var result6 = repository.FirstOrDefaultFromSubordinate(guid);
                //var result7 = repository.CountFromSubordinate(m => m.Name.Contains("23"));
                //var result8 = repository.ExistedFromSubordinate(guid);
                //var result9 = repository.FindFromSubordinate(m => m.Name.Contains("23"));
                //var result10 = repository.PagingFromSubordinate(m => m.Name.Contains("23"), m => m.CreateTime, SortOrder.Ascending, 1, 10);
            }
            catch (AggregateException ex)
            {
                Console.WriteLine(ex.InnerExceptions[0].Message);
            }
        }
    }
}
