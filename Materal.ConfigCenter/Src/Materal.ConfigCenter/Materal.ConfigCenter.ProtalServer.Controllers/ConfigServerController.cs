using AspectCore.DynamicProxy;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.ConfigCenter.ControllerCore;
using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;

namespace Materal.ConfigCenter.ProtalServer.Controllers
{
    public class ConfigServerController : ConfigCenterBaseController
    {
        private readonly IConfigServerService _configServerService;

        public ConfigServerController(IConfigServerService configServerService)
        {
            _configServerService = configServerService;
        }

        /// <summary>
        /// 注册客户端
        /// </summary>
        /// <returns></returns>
        [HttpGet, AllowAuthority]
        public ResultModel Register(string name)
        {
            try
            {
                ICharSequence charSequence = Request.Headers.Get(HttpHeaderNames.Referer, null);
                if (charSequence == null) throw new MateralConfigCenterException("未识别Referer");
                string Referer = charSequence.ToString();
                var client = new RegisterConfigServerModel
                {
                    Address = Referer,
                    Name = name
                };
                _configServerService.RegisterNewClient(client);
                return ResultModel.Success("注册成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }

    }
}
