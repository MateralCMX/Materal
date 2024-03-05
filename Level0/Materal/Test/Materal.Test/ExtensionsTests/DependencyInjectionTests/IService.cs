namespace Materal.Test.ExtensionsTests.DependencyInjectionTests
{
    public interface IService
    {
        void SayHello();
        void Test([Required(ErrorMessage = "消息为空")] string message = "Hello");
        void Test(TestModel model);
    }
    public class TestModel
    {
        [Required(ErrorMessage = "消息为空")]
        public string Message { get; set; } = string.Empty;
    }
}