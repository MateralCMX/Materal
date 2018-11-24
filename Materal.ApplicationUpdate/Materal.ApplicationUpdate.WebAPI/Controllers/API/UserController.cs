using System;
using Materal.ApplicationUpdate.DTO.User;
using Materal.ApplicationUpdate.Service;
using Materal.ApplicationUpdate.WebAPI.Models.User;
using Materal.Common;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Materal.ApplicationUpdate.WebAPI.Controllers.API
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="userService"></param>
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel<LoginUserDTO>> Login(UserLoginRequestModel requestModel)
        {
            throw new Exception("测试异常处理");
            //LoginUserDTO loginUser = await _userService.LoginAsync(null);
            //return loginUser == null ?
            //    ResultModel<LoginUserDTO>.Fail("登录失败，用户名或者密码错误。") :
            //    ResultModel<LoginUserDTO>.Success("登录成功。");
        }
    }
}
