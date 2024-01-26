using Materal.MergeBlock.GeneratorCode.Models;

namespace Materal.MergeBlock.Test
{
    [TestClass]
    public class GeneratorCodeTest
    {
        [TestMethod]
        [DataRow(@"D:\Project\Materal\Materal\Level4\Materal.MergeBlock\MMB\Demo\MMB.Demo.Abstractions\Domain\User.cs")]
        //[DataRow(@"D:\Project\Materal\Materal\Level4\MMB\MMB.Core\Demo\MMB.Demo.Abstractions\Domain\User.cs")]
        public void GeneratorCodeByDomain(string domainPath)
        {
            string[] codes = File.ReadAllLines(domainPath);
            DomainModel domainModel = new(codes);
            Assert.AreEqual(domainModel.Name, "User");
        }
    }
}