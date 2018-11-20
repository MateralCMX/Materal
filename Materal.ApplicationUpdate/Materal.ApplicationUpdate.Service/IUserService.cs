using System.Threading.Tasks;
using Materal.ApplicationUpdate.DTO.User;

namespace Materal.ApplicationUpdate.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="password">密码</param>
        /// <returns>登录用户信息</returns>
        Task<LoginUserDTO> Login(string account, string password);
        /// <summary>
        /// 是否登录
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> IsLogin(string token);
        /// <summary>
        /// 根据Token获取用户信息
        /// </summary>
        /// <param name="token">Token值</param>
        /// <returns>用户信息</returns>
        Task<LoginUserDTO> GetUserInfoByToken(string token);
    }
}
