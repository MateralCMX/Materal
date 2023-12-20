using Materal.MergeBlock;

namespace MMB.Demo.WebAPI
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
        public static async Task Main(string[] args) => await MergeBlockProgram.RunAsync(args);
    }
}
