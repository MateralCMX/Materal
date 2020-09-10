using AutoMapper;
using Materal.APP.PresentationModel.Server;
using Materal.APP.Services.Models.Server;

namespace Materal.APP.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ServerProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public ServerProfile()
        {
            CreateMap<RegisterRequestModel, RegisterModel>();
        }
    }
}
