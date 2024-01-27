using Materal.MergeBlock.NormalModule;

namespace Materal.MergeBlock.NormalTest
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
        public static Task Main(string[] args) => new MergeBlockNormalProgram().RunAsync(args);
    }
}
