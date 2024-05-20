using Materal.TTA.Demo.Domain;
using Materal.Utils.Model;

namespace Materal.TTA.Demo.Tests
{
    public class Test02(ITestDomainRepository testDomainRepository) : ITTADemoTest
    {
        public async Task RunTestAsync()
        {
            List<Guid> ids = [];
            List<TestDomain> testDomains = await testDomainRepository.FindAsync(m => ids.Contains(m.ID));
            (List<TestDomain> data, RangeModel rangeInfo) = await testDomainRepository.RangeAsync(m => true, 0, 2);
        }
    }
}
