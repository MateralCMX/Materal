using AutoMapper;
using Materal.ConDep.Center.PresentationModel.Server;
using Materal.ConDep.Center.Services.Models.Server;

namespace Materal.ConDep.Center.Controllers.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ServerProfile : Profile
    {
        public ServerProfile()
        {
            CreateMap<RegisterServerRequestModel, ServerModel>();
        }
    }
}
