using AutoMapper;
using WeChatService.DataTransmitModel.WeChatDomain;
using WeChatService.Domain;
namespace WeChatService.ServiceImpl.AutoMapperProfile
{
    /// <summary>
    /// 微信域名AutoMapper配置
    /// </summary>
    public sealed class WeChatDomainProfile : Profile
    {
        public WeChatDomainProfile()
        {
            CreateMap<WeChatDomain, WeChatDomainListDTO>();
            CreateMap<WeChatDomain, WeChatDomainDTO>();
        }
    }
}
