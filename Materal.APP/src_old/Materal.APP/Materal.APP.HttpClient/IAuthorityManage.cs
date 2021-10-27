using System.Threading.Tasks;

namespace Materal.APP.HttpClient
{
    public interface IAuthorityManage
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <returns></returns>
        Task<bool> IsLoginAsync();

        /// <summary>
        /// 设置Token
        /// </summary>
        /// <param name="token"></param>
        Task SetTokenAsync(string token);
        /// <summary>
        /// 获取Token
        /// </summary>
        /// <returns></returns>
        Task<string> GetTokenAsync();
        /// <summary>
        /// 移除Token
        /// </summary>
        Task RemoveTokenAsync(); 
    }
}
