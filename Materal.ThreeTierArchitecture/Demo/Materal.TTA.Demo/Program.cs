using Materal.Common;
using Materal.ConvertHelper;
using Materal.TTA.Demo.Domain;
using Materal.TTA.Demo.Sqlite;
using Materal.TTA.EFRepository;
using Materal.TTA.SqliteRepository.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Materal.TTA.Demo
{
    public class Program
    {
        public static async Task Main()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
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
                });
            }, ServiceLifetime.Transient);
            serviceCollection.AddTransient<IUserRepository, UserSqliteRepository>();//Sqlite
            #endregion
            serviceCollection.AddTransient<IDemoUnitOfWork, DemoUnitOfWorkImpl>();
            IServiceProvider services = serviceCollection.BuildServiceProvider();

            MigrateHelper<TTADemoDBContext> migrateHelper = services.GetService<MigrateHelper<TTADemoDBContext>>() ?? throw new MateralException("获取实例失败");
            IDemoUnitOfWork _unitOfWork = services.GetService<IDemoUnitOfWork>() ?? throw new MateralException("获取实例失败");
            IUserRepository _userRepository = services.GetService<IUserRepository>() ?? throw new MateralException("获取实例失败");
            await migrateHelper.MigrateAsync();
            User? user = new()
            {
                Name = "Materal"
            };
            _unitOfWork.RegisterAdd(user);
            await _unitOfWork.CommitAsync();
            user = _userRepository.FirstOrDefault(user.ID);
            if (user == null) throw new MateralException("数据获取失败");
            Console.WriteLine(user.ToJson());
        }
    }
}