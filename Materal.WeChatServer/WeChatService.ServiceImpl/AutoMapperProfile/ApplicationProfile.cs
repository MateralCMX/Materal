using AutoMapper;
using WeChatService.DataTransmitModel.Application;
using WeChatService.Domain;
namespace WeChatService.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 应用AutoMapper配置
    /// </summary>
    public sealed class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            CreateMap<Application, ApplicationListDTO>();
            CreateMap<Application, ApplicationDTO>();
        }
    }
}
