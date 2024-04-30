using Materal.MergeBlock.WebModule;

namespace Materal.MergeBlock.WebAPITest
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}
