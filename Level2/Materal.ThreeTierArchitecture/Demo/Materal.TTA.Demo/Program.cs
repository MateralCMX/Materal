using Materal.Abstractions;
using Materal.TTA.Demo.Domain;
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

            //serviceCollection.AddSqliteEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteEFHelper.MigrateAsync(serviceProvider);

            serviceCollection.AddSqlServerEFTTA();
            static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerEFHelper.MigrateAsync(serviceProvider);

            IServiceProvider services = serviceCollection.BuildServiceProvider();
            using(IServiceScope scope = services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                await MigrateAsync(serviceProvider);
                IDemoUnitOfWork _unitOfWork = serviceProvider.GetService<IDemoUnitOfWork>() ?? throw new MateralException("获取实例失败");
                ITestDomainRepository _userRepository = _unitOfWork.GetRepository<ITestDomainRepository>();
                TestDomain? user = new()
                {
                    StringType = "String",
                    ByteType = 1,
                    IntType = 2,
                    DateTimeType = DateTime.Now,
                    DecimalType = 5.56m,
                    EnumType = TestEnum.Type2,
                    ID = Guid.NewGuid()
                };
                _unitOfWork.RegisterAdd(user);
                await _unitOfWork.CommitAsync();
                user = _userRepository.FirstOrDefault(user.ID);
                if (user == null) throw new MateralException("数据获取失败");
                Console.WriteLine(user.ToJson());
                List<TestDomain> users = await _userRepository.FindAsync(m => true);
                Console.WriteLine(users.ToJson());
            }
            Console.ReadKey();
        }
    }
}