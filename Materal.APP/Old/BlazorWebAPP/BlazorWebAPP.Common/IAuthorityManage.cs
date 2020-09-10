using System.Threading.Tasks;
using Grpc.Core;

namespace BlazorWebAPP.Common
{
    public interface IAuthorityManage
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        public Task<bool> IsLoginAsync();

        /// <summary>
        /// 设置Token
        /// </summary>
        /// <param name="token"></param>
        public Task SetTokenAsync(string token);
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        public Task<string> GetTokenAsync();
        /// <summary>
        /// 移除Token
        /// </summary>
        public Task RemoveTokenAsync();
        /// <summary>
        /// GrpcMetadata
        /// </summary>
        /// <returns></returns>
        public Metadata GrpcHeaders { get; }
    }
}
