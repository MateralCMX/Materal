using System.ComponentModel.DataAnnotations;

namespace Materal.Model.Example
{
    public class TestServiceImpl : ITestService
    {
        public void Test01([Required]string[] strings)
        {
        }
    }
}
