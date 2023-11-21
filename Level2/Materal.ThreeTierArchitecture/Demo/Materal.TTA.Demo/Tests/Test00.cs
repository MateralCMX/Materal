using Materal.TTA.Demo.Domain;

namespace Materal.TTA.Demo.Tests
{
    public class Test00(IDemoUnitOfWork unitOfWork, ITestDomainRepository testDomainRepository) : ITTADemoTest
    {
        public async Task RunTestAsync()
        {
            List<TestDomain> testDomains = await testDomainRepository.FindAsync(m => true);
            if (testDomains.Count <= 0) return;
            foreach (TestDomain testDomain in testDomains)
            {
                unitOfWork.RegisterDelete(testDomain);
            }
            await unitOfWork.CommitAsync();
        }
    }
}
