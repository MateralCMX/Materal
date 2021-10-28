using Materal.APP.WebAPICore;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Authority.Server
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
        public static async Task Main(string[] args)
        {
            IHostBuilder hostBuilder = MateralAPPWebAPIProgram.CreateHostBuilder<Startup>(args);
            IHost host = hostBuilder.Build();
            await host.RunAsync();
        }
    }
}
