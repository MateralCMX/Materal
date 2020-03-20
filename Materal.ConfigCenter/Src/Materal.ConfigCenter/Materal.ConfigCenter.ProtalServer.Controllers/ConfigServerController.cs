using AspectCore.DynamicProxy;
using DotNetty.Codecs.Http;
using DotNetty.Common.Utilities;
using Materal.ConfigCenter.ControllerCore;
using Materal.ConfigCenter.ProtalServer.PresentationModel.ConfigServer;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConfigCenter.ProtalServer.Services.Models;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        /// <summary>
        /// 获得客户端列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<ConfigServerModel>> GetConfigServerList()
        {
            try
            {
                List<ConfigServerModel> result = _configServerService.GetConfigServers();
                return ResultModel<List<ConfigServerModel>>.Success(result, "查询成功");
            }
            catch (AspectInvocationException ex)
            {
                return ResultModel<List<ConfigServerModel>>.Fail(ex.InnerException?.Message);
            }
            catch (MateralConfigCenterException ex)
            {
                return ResultModel<List<ConfigServerModel>>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 复制配置服务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CopyConfigServer(CopyConfigServerModel model)
        {
            try
            {
                string token = GetToken();
                await _configServerService.CopyConfigServer(model, token);
                return ResultModel.Success("复制成功");
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
        /// <summary>
        /// 复制命名空间
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ResultModel> CopyNamespace(CopyNamespaceModel model)
        {
            try
            {
                string token = GetToken();
                await _configServerService.CopyNamespace(model, token);
                return ResultModel.Success("复制成功");
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
