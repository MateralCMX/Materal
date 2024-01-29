using Materal.MergeBlock.ConsoleModule;

namespace MMB.Demo.App
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await new MergeBlockConsoleProgram().RunAsync(args);
    }
}