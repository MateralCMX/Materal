namespace Materal.BusinessFlow.TestConsole
{
    public class Program
    {
        public static async Task Main()
        {
            ITestHandler testHandler = new NodeRunConditionTestHandler();
            await testHandler.ExcuteAsync();
            Console.WriteLine("测试结束,按任意键退出");
            Console.ReadKey();
        }
    }
}