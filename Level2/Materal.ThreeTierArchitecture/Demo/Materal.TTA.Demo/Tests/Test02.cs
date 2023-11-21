using Materal.TTA.Demo.Domain;

namespace Materal.TTA.Demo.Tests
{
    public class Test02(ITestDomainRepository testDomainRepository) : ITTADemoTest
    {
        public async Task RunTestAsync()
        {
            List<Guid> ids = [];
            List<TestDomain> testDomains = await testDomainRepository.FindAsync(m => ids.Contains(m.ID));
        }
    }
}
