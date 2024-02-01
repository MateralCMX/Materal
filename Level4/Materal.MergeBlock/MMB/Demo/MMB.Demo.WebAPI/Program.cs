using Materal.MergeBlock.WebModule;

namespace MMB.Demo.WebAPI
{
    /// <summary>
    /// 主程序
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口
        /// </summary>
        /// <param name="args"></param>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}
