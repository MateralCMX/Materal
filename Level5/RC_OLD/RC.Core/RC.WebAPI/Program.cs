using Materal.MergeBlock.WebModule;

namespace RC.WebAPI
{
    /// <summary>
    /// ������
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}