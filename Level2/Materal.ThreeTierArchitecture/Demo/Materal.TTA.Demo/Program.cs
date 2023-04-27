using Materal.Abstractions;
using Materal.TTA.Common.Model;
using Materal.TTA.Demo.Domain;
using Materal.TTA.Demo.Sqlite;
using Materal.TTA.EFRepository;
using Materal.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.Demo
{
    public class Program
    {
        public static async Task Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralUtils();
            serviceCollection.AddTransient<MigrateHelper<TTADemoDBContext>>();
            #region sqlite
            SqliteConfigModel dbConfig = new()
            {
                Source = "DemoDB.db"
            };
            serviceCollection.AddDbContext<TTADemoDBContext>(options =>
            {
                options.UseSqlite(dbConfig.ConnectionString, m =>
                {
                    m.CommandTimeout(300);
                })
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });
            serviceCollection.AddScoped<IUserRepository, UserSqliteRepository>();//Sqlite
            #endregion
            serviceCollection.AddScoped<IDemoUnitOfWork, DemoUnitOfWorkImpl>();
            IServiceProvider services = serviceCollection.BuildServiceProvider();
            using(IServiceScope scope = services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                MigrateHelper<TTADemoDBContext> migrateHelper = serviceProvider.GetService<MigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
                await migrateHelper.MigrateAsync();
                IDemoUnitOfWork _unitOfWork = serviceProvider.GetService<IDemoUnitOfWork>() ?? throw new MateralException("获取实例失败");
                IUserRepository _userRepository = _unitOfWork.GetRepository<IUserRepository>();
                User? user = new()
                {
                    Name = "Materal"
                };
                _unitOfWork.RegisterAdd(user);
                await _unitOfWork.CommitAsync();
                user = _userRepository.FirstOrDefault(user.ID);
                if (user == null) throw new MateralException("数据获取失败");
                Console.WriteLine(user.ToJson());
                List<User> users = await _userRepository.FindAsync(m => true);
                Console.WriteLine(users.ToJson());
            }
            Console.ReadKey();
        }
    }
}