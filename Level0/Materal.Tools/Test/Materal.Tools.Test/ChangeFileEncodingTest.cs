using Materal.Tools.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;

namespace Materal.Tools.Test
{
    [TestClass]
    public class ChangeFileEncodingTest
    {
        [TestMethod]
        public async Task TestAsync()
        {
            await ChangeEncoding.RunAsync("D:\\Test", Encoding.UTF8, ".+\\.cs", true);
        }
    }
}
