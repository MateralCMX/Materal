using Materal.APP.Common;
using Materal.ConvertHelper;
using Materal.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Materal.APP.Server.Controllers
{
    [Route("api/[controller]/[action]"), ApiController]
    public class StringHelperController : MateralAPPBaseController
    {
        public ResultModel<string> DesDecryption(StringHandlerRequestModel requestModel)
        {
            string result = requestModel.Value.DesDecode(ApplicationConfig.DesKey, ApplicationConfig.DesIV, Encoding.UTF8);
            return ResultModel<string>.Success(result, "解密成功");
        }
    }

    public class StringHandlerRequestModel
    {
        /// <summary>
        /// 值
        /// </summary>
        [Required(ErrorMessage = "值不能为空")]
        public string Value { get; set; }
    }
}
