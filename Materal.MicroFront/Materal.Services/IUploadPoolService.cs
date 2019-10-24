using System.Threading.Tasks;
using DotNetty.Transport.Channels;
using Materal.MicroFront.Commands;

namespace Materal.Services
{
    /// <summary>
    /// 上传池服务
    /// </summary>
    public interface IUploadPoolService
    {
        /// <summary>
        /// 新的上传
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="command"></param>
        Task NewUpload(IChannel channel, UploadStartCommand command);
        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        Task LoadBuffer(IChannel channel, UploadPartCommand command);
    }
}
