using AutoMapper;
using Authority.PresentationModel.User.Request;
using Authority.Service.Model.User;
namespace Authority.PresentationModel.AutoMapperProfile
{
    /// <summary>
    /// 用户AutoMapper配置
    /// </summary>
    public sealed class UserProfile : Profile
    {
        /// <summary>
        /// 用户AutoMapper配置
        /// </summary>
        public UserProfile()
        {
            CreateMap<AddUserRequestModel, AddUserModel>();
            CreateMap<EditUserRequestModel, EditUserModel>();
            CreateMap<QueryUserFilterRequestModel, QueryUserFilterModel>();
            CreateMap<ExchangePasswordRequestModel, ExchangePasswordModel>();
        }
    }
}
