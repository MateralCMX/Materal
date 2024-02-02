using Materal.MergeBlock.WebModule;

namespace Materal.Gateway.WebAPI
{
    /// <summary>
    /// 入口类
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}