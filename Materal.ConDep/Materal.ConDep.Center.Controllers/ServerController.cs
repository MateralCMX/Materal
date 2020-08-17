using AutoMapper;
using Materal.ConDep.Center.DataTransmitModel.Server;
using Materal.ConDep.Center.PresentationModel.Server;
using Materal.ConDep.Center.Services;
using Materal.ConDep.Center.Services.Models.Server;
using Materal.DotNetty.ControllerBus.Attributes;
using Materal.Model;
using System.Collections.Generic;

namespace Materal.ConDep.Center.Controllers
{
    public class ServerController : ConDepBaseController
    {
        private readonly IMapper _mapper;
        private readonly IServerManage _serverManage;

        public ServerController(IMapper mapper, IServerManage serverManage)
        {
            _mapper = mapper;
            _serverManage = serverManage;
        }

        /// <summary>
        /// 获取系统信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ResultModel<List<ServerListDTO>> GetServerList()
        {
            try
            {
                List<ServerListDTO> result = _serverManage.GetList();
                return ResultModel<List<ServerListDTO>>.Success(result, "获取成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel<List<ServerListDTO>>.Fail(ex.Message);
            }
        }
        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost, AllowAuthority]
        public ResultModel RegisterServer(RegisterServerRequestModel requestModel)
        {
            try
            {
                var model = _mapper.Map<ServerModel>(requestModel);
                _serverManage.RegisterServer(model);
                return ResultModel.Success("注册成功");
            }
            catch (MateralConDepException ex)
            {
                return ResultModel.Fail(ex.Message);
            }
        }
    }
}
