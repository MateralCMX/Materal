using Materal.MergeBlock.WebModule;

namespace Materal.Gateway.WebAPI
{
    /// <summary>
    /// �����
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ��ڷ���
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static async Task Main(string[] args) => await new MergeBlockWebProgram().RunAsync(args);
    }
}