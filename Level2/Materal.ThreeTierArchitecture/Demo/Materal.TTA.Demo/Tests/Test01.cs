using Materal.Abstractions;
using Materal.Extensions;
using Materal.TTA.Demo.Domain;
using Materal.Utils.Model;

namespace Materal.TTA.Demo.Tests
{
    public class Test01(IDemoUnitOfWork unitOfWork, ITestDomainRepository testDomainRepository) : ITTADemoTest
    {
        public async Task RunTestAsync()
        {
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
            for (int i = 0; i < 40; i++)
            {
                domain = new()
                {
                    StringType = $"String{i}",
                    ByteType = 1,
                    IntType = 2,
                    DateTimeType = DateTime.Now,
                    DecimalType = 5.56m,
                    EnumType = TestEnum.Type2,
                    ID = Guid.NewGuid()
                };
                unitOfWork.RegisterAdd(domain);
            }
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
    }
}
