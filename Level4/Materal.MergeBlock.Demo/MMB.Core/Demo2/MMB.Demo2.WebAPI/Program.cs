using Materal.MergeBlock;

namespace MMB.Demo2.WebAPI
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
        public static async Task Main(string[] args) => await MergeBlockProgram.RunAsync(args);
    }
}
