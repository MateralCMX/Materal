using Materal.MergeBlock.WebModule;

namespace MMB.Demo.WebAPI
{
    /// <summary>
    /// 程序入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口方法
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}
