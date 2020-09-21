using Materal.APP.Core;
using Materal.APP.PresentationModel.StringHelper;
using Materal.APP.WebAPICore;
using Materal.ConvertHelper;
using Materal.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using Materal.APP.HttpManage;

namespace Materal.APP.Server.Controllers
{
    /// <summary>
    /// 字符串帮助控制器
    /// </summary>
    [Route("api/[controller]/[action]"), ApiController]
    public class StringHelperController : WebAPIControllerBase, IStringHelperManage
    {
        /// <summary>
        /// Des字符串解密
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public Task<ResultModel<string>> DesDecryptionAsync(StringRequestModel requestModel)
        {
            return Task.Run(() =>
            {
                try
                {
                    string result = requestModel.Value.DesDecode(ApplicationConfig.DesKey, ApplicationConfig.DesIV, Encoding.UTF8);
                    return ResultModel<string>.Success(result, "解密成功");
                }
                catch (Exception)
                {
                    return ResultModel<string>.Fail("解密失败");
                }
            });
        }
        /// <summary>
        /// Des字符串加密
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<ResultModel<string>> DesEncryptionAsync(StringRequestModel requestModel)
        {
            return Task.Run(() =>
            {
                try
                {
                    string result = requestModel.Value.ToDesEncode(ApplicationConfig.DesKey, ApplicationConfig.DesIV, Encoding.UTF8);
                    return ResultModel<string>.Success(result, "加密成功");
                }
                catch (Exception)
                {
                    return ResultModel<string>.Fail("加密失败");
                }
            });
        }
    }
}
