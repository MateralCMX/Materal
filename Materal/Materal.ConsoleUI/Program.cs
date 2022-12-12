using Materal.ConvertHelper;
using Materal.Model;

namespace Materal.ConsoleUI
{
    internal class Program
    {
        public static void Main()
        {
            TestMode test = new()
            {
                Name = "Test",
            };
            var a = test.GetSearchExpression<Test>();
        }
    }
    public class TestMode : FilterModel
    {
        [Equal]
        public string? Name { get; set; }
    }
    public class Test
    {
        public string Name { get; set; } = string.Empty;
    }
}
