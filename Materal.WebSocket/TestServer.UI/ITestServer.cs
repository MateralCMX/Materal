using System.Threading.Tasks;

namespace TestServer.UI
{
    public interface ITestServer
    {
        /// <summary>
        /// 启动
        /// </summary>
        /// <returns></returns>
        Task RunServerAsync();
    }
}
