using Materal.MergeBlock.GeneratorCode.Models;
using Materal.Test.Base;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Materal.MergeBlock.GeneratorCode.Test
{
    [TestClass]
    public class ResolutionTest : MateralTestBase
    {
        private const string _filePath = @"E:\Project\Materal\Materal\Materal\Test\Materal.MergeBlock.GeneratorCode.Test\Test.cs";
        [TestMethod]
        public void ResolutionDomainTest()
        {
            List<CSharpCodeFileModel> codeFileModels = CSharpFileParser.ParseByFilePath(_filePath);
        }
    }
}
