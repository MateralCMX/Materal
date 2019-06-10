using AutoMapper;
using WeChatService.PresentationModel.WeChatDomain.Request;
using WeChatService.Service.Model.WeChatDomain;
namespace WeChatService.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 微信域名AutoMapper配置
    /// </summary>
    public sealed class WeChatDomainProfile : Profile
    {
        /// <summary>
        /// 微信域名AutoMapper配置
        /// </summary>
        public WeChatDomainProfile()
        {
            CreateMap<AddWeChatDomainRequestModel, AddWeChatDomainModel>();
            CreateMap<EditWeChatDomainRequestModel, EditWeChatDomainModel>();
        }
    }
}
