using AutoMapper;
using WeChatService.PresentationModel.Application.Request;
using WeChatService.Service.Model.Application;
namespace WeChatService.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 应用AutoMapper配置
    /// </summary>
    public sealed class ApplicationProfile : Profile
    {
        /// <summary>
        /// 应用AutoMapper配置
        /// </summary>
        public ApplicationProfile()
        {
            CreateMap<AddApplicationRequestModel, AddApplicationModel>();
            CreateMap<EditApplicationRequestModel, EditApplicationModel>();
            CreateMap<QueryApplicationFilterRequestModel, QueryApplicationFilterModel>();
        }
    }
}
