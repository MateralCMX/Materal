using Materal.MergeBlock.WebModule;

namespace MMB.Demo.WebAPI
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
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}
