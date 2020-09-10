using Authority.Common;
using Authority.DataTransmitModel.User;
using Authority.GrpcModel;
using Authority.Services;
using Authority.Services.Models.User;
using AutoMapper;
using Grpc.Core;
using Materal.Model;
using Materal.StringHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Materal.APP.Common;
using Google.Protobuf.WellKnownTypes;

namespace Authority.Server.Services
{
    public class UserService : User.UserBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        public UserService(IMapper mapper, IUserService userService)
        {
            _mapper = mapper;
            _userService = userService;
        }
        /// <summary>
        /// ����û�
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<VoidResultModel> AddUser(AddUserGrpcRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                var model = _mapper.Map<AddUserModel>(request);
                await _userService.AddUserAsync(model);
                return GrpcServiceHelper.GetResultModel<VoidResultModel>("��ӳɹ�");
            });
        }
        /// <summary>
        /// �޸��û�
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<VoidResultModel> EditUser(EditUserGrpcRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                var model = _mapper.Map<EditUserModel>(request);
                await _userService.EditUserAsync(model);
                return GrpcServiceHelper.GetResultModel<VoidResultModel>("�޸ĳɹ�");
            });
        }
        /// <summary>
        /// ɾ���û�
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<VoidResultModel> DeleteUser(OnlyOperatingRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                if (!request.ID.IsGuid()) throw new AuthorityException("ID��ʽ����ȷ");
                await _userService.DeleteUserAsync(Guid.Parse(request.ID));
                return GrpcServiceHelper.GetResultModel<VoidResultModel>("ɾ���ɹ�");
            });
        }
        /// <summary>
        /// ����û���Ϣ
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UserInfoGrpcResultModel> GetUserInfo(OnlyOperatingRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                if (!request.ID.IsGuid()) throw new AuthorityException("ID��ʽ����ȷ");
                UserDTO userInfo = await _userService.GetUserInfoAsync(Guid.Parse(request.ID));
                var result = GrpcServiceHelper.GetResultModel<UserInfoGrpcResultModel>("��ȡ�ɹ�");
                result.Data = _mapper.Map<UserGrpcModel>(userInfo);
                return result;
            });
        }
        /// <summary>
        /// ����û��б�
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UserListGrpcResultModel> GetUserList(QueryUserFilterGrpcRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                var model = _mapper.Map<QueryUserFilterModel>(request);
                (List<UserListDTO> userInfo, PageModel pageModel) = await _userService.GetUserListAsync(model);
                var result = GrpcServiceHelper.GetResultModel<UserListGrpcResultModel>("��ȡ�ɹ�");
                result.Data.AddRange(_mapper.Map<List<UserGrpcModel>>(userInfo));
                result.PageInfo = _mapper.Map<PageInfoModel>(pageModel);
                return result;
            });
        }
        /// <summary>
        /// ����ҵ���Ϣ
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<UserInfoGrpcResultModel> GetMyUserInfo(Empty request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                Guid loginUserID = GrpcServiceHelper.GetLoginUserID(context);
                UserDTO userInfo = await _userService.GetUserInfoAsync(loginUserID);
                var result = GrpcServiceHelper.GetResultModel<UserInfoGrpcResultModel>("��ȡ�ɹ�");
                result.Data = _mapper.Map<UserGrpcModel>(userInfo);
                return result;
            });
        }
        /// <summary>
        /// ��¼
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public override async Task<TokenGrpcResultModel> Login(LoginGrpcRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                var model = _mapper.Map<LoginModel>(request);
                UserDTO userInfo = await _userService.LoginAsync(model);
                string token = GetToken(userInfo);
                return new TokenGrpcResultModel
                {
                    Status = 0,
                    Message = "��¼�ɹ�",
                    Token = token,
                    ExpiredTime = ApplicationConfig.JWTConfig.ExpiredTime
                };
            }, "�û������������");
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<ResetPasswordGrpcResultModel> ResetPassword(OnlyOperatingRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                if (!request.ID.IsGuid()) throw new AuthorityException("ID��ʽ����ȷ");
                string password = await _userService.ResetPasswordAsync(Guid.Parse(request.ID));
                var result = GrpcServiceHelper.GetResultModel<ResetPasswordGrpcResultModel>("���óɹ�");
                result.NewPassword = password;
                return result;
            });
        }
        /// <summary>
        /// �޸�����
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<VoidResultModel> ChangePassword(ChangePasswordGrpcRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                var model = _mapper.Map<ChangePasswordModel>(request);
                model.ID = GrpcServiceHelper.GetLoginUserID(context);
                await _userService.ChangePasswordAsync(model);
                return GrpcServiceHelper.GetResultModel<VoidResultModel>("�޸ĳɹ�");
            });
        }
        /// <summary>
        /// �޸��ҵ���Ϣ
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<VoidResultModel> EditMyInfo(EditMyInfoGrpcRequestModel request, ServerCallContext context)
        {
            return await GrpcServiceHelper.HandlerAsync(async () =>
            {
                var model = _mapper.Map<EditUserModel>(request);
                model.ID = GrpcServiceHelper.GetLoginUserID(context);
                await _userService.EditUserAsync(model);
                return GrpcServiceHelper.GetResultModel<VoidResultModel>("�޸ĳɹ�");
            });
        }
        #region ˽�з���
        /// <summary>
        /// ���Token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GetToken(UserListDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddSeconds(ApplicationConfig.JWTConfig.ExpiredTime);
            var securityKey = new SymmetricSecurityKey(ApplicationConfig.JWTConfig.KeyBytes);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Aud,ApplicationConfig.JWTConfig.Audience),
                    new Claim(JwtRegisteredClaimNames.Iss,ApplicationConfig.JWTConfig.Issuer),
                    new Claim("UserID",user.ID.ToString())
                }),
                Audience = ApplicationConfig.JWTConfig.Audience,
                Issuer = ApplicationConfig.JWTConfig.Issuer,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
            };
            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            string tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
        #endregion
    }
}
