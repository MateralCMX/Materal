using Materal.MergeBlock.WebModule;

namespace RC.WebAPI
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
        /// <returns></returns>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}