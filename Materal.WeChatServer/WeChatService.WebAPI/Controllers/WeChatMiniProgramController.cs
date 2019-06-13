using System;
using System.Threading.Tasks;
using Authority.PresentationModel;
using Authority.Service;
using Common.Model.APIAuthorityConfig;
using Materal.Common;
using Materal.WeChatHelper;
using Materal.WeChatHelper.Model;
using Microsoft.AspNetCore.Mvc;
using WeChatService.DataTransmitModel.Application;
using WeChatService.PresentationModel.WeChatMiniProgram.Request;
using WeChatService.Service;

namespace WeChatService.WebAPI.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// 微信小程序控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class WeChatMiniProgramController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IApplicationService _applicationService;

        /// <summary>
        /// 构造方法
        /// </summary>
        public WeChatMiniProgramController(IUserService userService, IApplicationService applicationService)
        {
            _userService = userService;
            _applicationService = applicationService;
        }
        /// <summary>
        /// 获取OpenID
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AuthorityFilter(WeChatServiceAPIAuthorityConfig.GetWeChatMiniProgramOpenIDCode)]
        public async Task<ResultModel<string>> GetOpenID(GetOpenIDRequestModel requestModel)
        {
            try
            {
                Guid userID = _userService.GetUserID(requestModel.Token);
                ApplicationDTO applicationInfo =
                    await _applicationService.GetApplicationInfoAsync(requestModel.AppID, userID);
                if (applicationInfo == null) return ResultModel<string>.Fail("未注册该小程序");
                var weChatConfig = new WeChatConfigModel
                {
                    APPID = applicationInfo.AppID,
                    APPSECRET = applicationInfo.AppSecret
                };
                var manager = new WeChatMiniProgramManager(weChatConfig);
                string openID = manager.GetOpenIDByCode(requestModel.Code);
                return ResultModel<string>.Success(openID, "获取OpenID成功");
            }
            catch (InvalidOperationException ex)
            {
                return ResultModel<string>.Fail(ex.Message);
            }
            catch (WeChatException ex)
            {
                return ResultModel<string>.Fail(ex.Message);
            }
        }
    }
}
