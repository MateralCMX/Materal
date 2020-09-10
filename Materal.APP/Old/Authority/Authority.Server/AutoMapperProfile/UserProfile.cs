using System;
using Authority.DataTransmitModel.User;
using Authority.GrpcModel;
using Authority.Services.Models.User;
using AutoMapper;

namespace Authority.Server.AutoMapperProfile
{
    public sealed class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AddUserGrpcRequestModel, AddUserModel>();
            CreateMap<EditUserGrpcRequestModel, EditUserModel>()
                .ForMember(m => m.ID, options =>
                {
                    options.MapFrom(m => Guid.Parse(m.ID));
                });
            CreateMap<QueryUserFilterGrpcRequestModel, QueryUserFilterModel>();
            CreateMap<ChangePasswordGrpcRequestModel, ChangePasswordModel>();
            CreateMap<LoginGrpcRequestModel, LoginModel>();
            CreateMap<EditMyInfoGrpcRequestModel, EditUserModel>();

            CreateMap<UserListDTO, UserGrpcModel>()
                .ForMember(m => m.ID, options =>
                {
                    options.MapFrom(m => m.ID.ToString());
                });
            CreateMap<UserDTO, UserGrpcModel>()
                .ForMember(m => m.ID, options =>
                {
                    options.MapFrom(m => m.ID.ToString());
                });
        }
    }
}
