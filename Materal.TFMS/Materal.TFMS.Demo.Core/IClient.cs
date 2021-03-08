using System.Threading.Tasks;

namespace Materal.TFMS.Demo.Core
{
    public interface IClient
    {
        /// <summary>
        /// 发送事件
        /// </summary>
        Task SendEventAsync();
    }
}
