using Materal.Abstractions;
using Materal.Logger;
using Materal.TTA.Common;
using Materal.TTA.Demo.Domain;
using Materal.Utils;
using Materal.Utils.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Materal.TTA.Demo
{
    public class Program
    {
        public static async Task Main()
        {
            MateralConfig.PageStartNumber = 1;
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddMateralUtils();
            serviceCollection.AddMateralLogger();

            serviceCollection.AddSqliteEFTTA();
            static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerEFTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerEFHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqliteADONETTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqliteADONETHelper.MigrateAsync(serviceProvider);

            //serviceCollection.AddSqlServerADONETTTA();
            //static async Task MigrateAsync(IServiceProvider serviceProvider) => await SqlServerADONETHelper.MigrateAsync(serviceProvider);

            IServiceProvider services = serviceCollection.BuildServiceProvider();
            LoggerManager.Init(option =>
            {
                option.AddConsoleTarget("LifeConsole");
                option.AddAllTargetRule(LogLevel.Information, null, new[] { "Microsoft.EntityFrameworkCore.*" });
            });
            ILoggerExtension.TSQLLogLevel = LogLevel.Information;
            using (IServiceScope scope = services.CreateScope())
            {
                IServiceProvider serviceProvider = scope.ServiceProvider;
                await MigrateAsync(serviceProvider);
                IDemoUnitOfWork unitOfWork = serviceProvider.GetService<IDemoUnitOfWork>() ?? throw new MateralException("获取实例失败");
                ITestDomainRepository testDomainRepository = unitOfWork.GetRepository<ITestDomainRepository>();
                TestDomain? domain = new()
                {
                    StringType = "String",
                    ByteType = 1,
                    IntType = 2,
                    DateTimeType = DateTime.Now,
                    DecimalType = 5.56m,
                    EnumType = TestEnum.Type2,
                    ID = Guid.NewGuid()
                };
                unitOfWork.RegisterAdd(domain);
                await unitOfWork.CommitAsync();
                domain = testDomainRepository.FirstOrDefault(domain.ID);
                domain = testDomainRepository.FirstOrDefault(m => m.StringType.Equals("String"));
                if (domain == null) throw new MateralException("数据获取失败");
                domain.StringType = "String123";
                unitOfWork.RegisterEdit(domain);
                await unitOfWork.CommitAsync();
                Console.WriteLine(domain.ToJson());
                QueryTestModel queryModel = new()
                {
                    PageIndex = 1,
                    PageSize = 10,
                    SortPropertyName = nameof(TestDomain.DateTimeType),
                    IsAsc = false
                };
                (List<TestDomain> users, PageModel pageInfo) = await testDomainRepository.PagingAsync(queryModel);
                Console.WriteLine(users.ToJson());
            }
            Console.ReadKey();
        }
    }
}