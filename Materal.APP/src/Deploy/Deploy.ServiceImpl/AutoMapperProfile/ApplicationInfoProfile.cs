using AutoMapper;
using Deploy.DataTransmitModel.ApplicationInfo;
using Deploy.Domain;
using Deploy.ServiceImpl.Models;

namespace Deploy.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// AutoMapper配置
    /// </summary>
    public sealed class ApplicationInfoProfile : Profile
    {
        public ApplicationInfoProfile()
        {
            CreateMap<ApplicationInfo, ApplicationInfoModel>();
            CreateMap<ApplicationInfoModel, ApplicationInfoListDTO>();
            CreateMap<ApplicationInfoModel, ApplicationInfoDTO>();
        }
    }
}
