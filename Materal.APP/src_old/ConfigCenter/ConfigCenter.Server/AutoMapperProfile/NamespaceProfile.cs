using AutoMapper;
using ConfigCenter.PresentationModel.Namespace;
using ConfigCenter.Services.Models.Namespace;

namespace ConfigCenter.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class NamespaceProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public NamespaceProfile()
        {
            CreateMap<AddNamespaceRequestModel, AddNamespaceModel>();
            CreateMap<EditNamespaceRequestModel, EditNamespaceModel>();
            CreateMap<QueryNamespaceFilterRequestModel, QueryNamespaceFilterModel>();
        }
    }
}
