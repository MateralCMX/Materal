using AutoMapper;
using Materal.ConDep.Center.DataTransmitModel.Server;
using Materal.ConDep.Center.Services.Models.Server;

namespace Materal.ConDep.Center.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<ServerModel, ServerListDTO>();
        }
    }
}
