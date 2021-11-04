using AutoMapper;
using Deploy.PresentationModel.ApplicationInfo;
using Deploy.Services.Models.ApplicationInfo;

namespace Deploy.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ApplicationInfoProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public ApplicationInfoProfile()
        {
            CreateMap<AddApplicationInfoRequestModel, AddApplicationInfoModel>();
            CreateMap<EditApplicationInfoRequestModel, EditApplicationInfoModel>();
            CreateMap<QueryApplicationInfoFilterRequestModel, QueryApplicationInfoFilterModel>();
        }
    }
}
