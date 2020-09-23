using AutoMapper;
using ConfigCenter.PresentationModel.ConfigCenter;
using ConfigCenter.Services.Models.ConfigCenter;

namespace ConfigCenter.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ConfigCenterProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public ConfigCenterProfile()
        {
            CreateMap<RegisterEnvironmentRequestModel, RegisterEnvironmentModel>();
            CreateMap<SyncRequestModel, SyncModel>();
            CreateMap<SyncProjectRequestModel, SyncProjectModel>();
            CreateMap<SyncNamespaceRequestModel, SyncNamespaceModel>();
            CreateMap<SyncConfigurationItemRequestModel, SyncConfigurationItemModel>();
        }
    }
}
