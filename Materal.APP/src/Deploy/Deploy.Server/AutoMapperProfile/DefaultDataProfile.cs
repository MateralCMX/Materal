using AutoMapper;
using Deploy.PresentationModel.DefaultData;
using Deploy.Services.Models.DefaultData;

namespace Deploy.Server.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class DefaultDataProfile : Profile
    {
        /// <summary>
        /// AutoMapper配置
        /// </summary>
        public DefaultDataProfile()
        {
            CreateMap<AddDefaultDataRequestModel, AddDefaultDataModel>();
            CreateMap<EditDefaultDataRequestModel, EditDefaultDataModel>();
            CreateMap<QueryDefaultDataFilterRequestModel, QueryDefaultDataFilterModel>();
        }
    }
}
