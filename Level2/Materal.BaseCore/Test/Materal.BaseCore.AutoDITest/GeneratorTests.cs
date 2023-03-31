using Materal.BaseCore.AutoDI;

namespace Materal.BaseCore.AutoDITest
{
    [UsesVerify]
    public class GeneratorTests
    {
        [Fact]
        public async Task ServiceImplDIGeneratorTest()
        {
            var source = @"using AutoDITest.R;
using Materal.BaseCore.AutoDI;

namespace AutoDITest
{
    [NoBaseAutoDI]
    public partial class TestServiceImpl
    {
        private readonly ITestR _testR;
    }
}";
            AutoDIGenerator generator = new();
            await TestHelper.Verify(source, generator);
        }
    }
}
