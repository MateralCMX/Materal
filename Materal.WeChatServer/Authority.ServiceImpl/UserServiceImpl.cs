using Authority.DataTransmitModel.User;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.EFRepository;
using Authority.Service;
using Authority.Service.Model.User;
using AutoMapper;
using Common;
using Common.Tree;
using Materal.Common;
using Materal.ConvertHelper;
using Materal.LinqHelper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public sealed class UserServiceImpl : IUserService
    {
        /// <summary>
        /// 默认密码
        /// </summary>
        private const string DefaultPassword = "123456";
        /// <summary>
        /// 加密盐
        /// </summary>
        private const string PasswordSalt = "Materal";
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IAuthorityUnitOfWork _authorityUnitOfWork;
        public UserServiceImpl(IUserRepository userRepository, IMapper mapper, IAuthorityUnitOfWork authorityUnitOfWork, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _authorityUnitOfWork = authorityUnitOfWork;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }
        public async Task AddUserAsync(AddUserModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (!string.IsNullOrEmpty(model.Account) && await _userRepository.CountAsync(m => m.Account == model.Account) > 0) throw new InvalidOperationException("账户重复");
            var user = model.CopyProperties<User>();
            user.Password = GetEncodePassword(DefaultPassword);
            AddUserRoles(user, model.RoleIDs);
            _authorityUnitOfWork.RegisterAdd(user);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task EditUserAsync(EditUserModel model)
        {
            if (string.IsNullOrEmpty(model.Name)) throw new InvalidOperationException("名称为空");
            if (await _userRepository.CountAsync(m => m.ID != model.ID && m.Account == model.Account) > 0) throw new InvalidOperationException("账户重复");
            User userFromDB = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDB == null) throw new InvalidOperationException("用户不存在");
            model.CopyProperties(userFromDB);
            userFromDB.UpdateTime = DateTime.Now;
            await EditUserRoles(userFromDB, model.RoleIDs);
            _authorityUnitOfWork.RegisterEdit(userFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task DeleteUserAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new InvalidOperationException("用户不存在");
            userFromDB.IsDelete = true;
            _authorityUnitOfWork.RegisterEdit(userFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<UserDTO> GetUserInfoAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new InvalidOperationException("用户不存在");
            var result = _mapper.Map<UserDTO>(userFromDB);
            List<Role> allRoles = await _roleRepository.GetAllInfoFromCacheAsync();
            List<UserRole> userOwnedRoles = await _userRoleRepository.WhereAsync(m => m.UserID == id).ToList();
            Guid[] userHasID = userOwnedRoles.Select(m => m.RoleID).ToArray();
            result.UserRoleTreeList = TreeHelper.GetTreeList<UserRoleTreeDTO, Role, Guid>(allRoles, null,
                webMenuAuthority =>
                {
                    var temp = webMenuAuthority.CopyProperties<UserRoleTreeDTO>(nameof(Role.Child), nameof(Role.RoleWebMenuAuthorities), nameof(Role.RoleAPIAuthorities), nameof(Role.RoleActionAuthorities), nameof(Role.UserRoles));
                    temp.Owned = userHasID.Contains(webMenuAuthority.ID);
                    return temp;
                });
            return result;
        }
        public async Task<UserDTO> GetUserInfoAsync(string token)
        {
            return await GetUserInfoAsync(GetUserID(token));
        }
        public Guid GetUserID(string token)
        {
            Guid userID;
            try
            {
                IDictionary<string, object> tokenDic = JWTHelper.DecodeJWT(token, ApplicationConfig.IdentityServer.Secret);
                userID = Guid.Parse(tokenDic["sub"].ToString());
                if (userID == Guid.Empty) throw new InvalidOperationException("未找到用户ID");
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Token错误", ex);
            }
            return userID;
        }
        public async Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel)
        {
            Expression<Func<User, bool>> expression = m => true;
            if (!string.IsNullOrEmpty(filterModel.Account))
            {
                expression = expression.And(m => m.Account == filterModel.Account);
            }
            if (!string.IsNullOrEmpty(filterModel.PhoneNumber))
            {
                expression = expression.And(m => EF.Functions.Like(m.PhoneNumber, $"%{filterModel.PhoneNumber}%"));
            }
            if (!string.IsNullOrEmpty(filterModel.Email))
            {
                expression = expression.And(m => EF.Functions.Like(m.Email, $"%{filterModel.Email}%"));
            }
            if (!string.IsNullOrEmpty(filterModel.Name))
            {
                expression = expression.And(m => EF.Functions.Like(m.Name, $"%{filterModel.Name}%"));
            }
            if (filterModel.Sex.HasValue)
            {
                expression = expression.And(m => m.Sex == filterModel.Sex.Value);
            }
            (List<User> usersFromDB, PageModel pageModel) = await _userRepository.PagingAsync(expression,m=>m.CreateTime,SortOrder.Descending, filterModel);
            return (_mapper.Map<List<UserListDTO>>(usersFromDB), pageModel);
        }
        public async Task ExchangePasswordAsync(ExchangePasswordModel model)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (GetEncodePassword(userFromDB.Password) != model.OldPassword) throw new InvalidOperationException("输入的旧密码错误");
            model.NewPassword = GetEncodePassword(model.NewPassword);
            if (model.NewPassword == userFromDB.Password) throw new InvalidOperationException("新密码与旧密码相同");
            userFromDB.Password = model.NewPassword;
            userFromDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(userFromDB);
            await _authorityUnitOfWork.CommitAsync();
        }
        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User userFormDB = await _userRepository.FirstOrDefaultAsync(id);
            userFormDB.Password = GetEncodePassword(DefaultPassword);
            userFormDB.UpdateTime = DateTime.Now;
            _authorityUnitOfWork.RegisterEdit(userFormDB);
            await _authorityUnitOfWork.CommitAsync();
            return DefaultPassword;
        }
        public async Task<UserListDTO> LoginAsync(string account, string password)
        {
            password = GetEncodePassword(password);
            User userFromDB = await _userRepository.FirstOrDefaultAsync(m => m.Account == account && m.Password == password);
            if (userFromDB == null) throw new InvalidOperationException("帐户名或密码错误");
            var result = _mapper.Map<UserListDTO>(userFromDB);
            return result;
        }
        public string GetEncodePassword(string password)
        {
            return (PasswordSalt + password + PasswordSalt).ToMd5_32Encode();
        }
        #region 私有方法
        /// <summary>
        /// 添加用户角色权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleIDs"></param>
        private void AddUserRoles(User user, IEnumerable<Guid> roleIDs)
        {
            if (user.UserRoles == null)
            {
                user.UserRoles = new List<UserRole>();
            }
            foreach (Guid id in roleIDs)
            {
                user.UserRoles.Add(new UserRole
                {
                    RoleID = id
                });
            }
        }
        /// <summary>
        /// 编辑用户角色权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="roleIDs"></param>
        /// <returns></returns>
        private async Task EditUserRoles(User user, Guid[] roleIDs)
        {
            user.UserRoles = await _userRoleRepository.WhereAsync(m => m.UserID == user.ID).ToList();
            List<Guid> userRoleIDs = user.UserRoles.Select(m => m.RoleID).ToList();
            List<Guid> deleteIDs = userRoleIDs.Except(roleIDs).ToList();
            List<UserRole> deleteModel = user.UserRoles.Where(m => deleteIDs.Contains(m.RoleID)).ToList();
            foreach (UserRole userRole in deleteModel)
            {
                _authorityUnitOfWork.RegisterDelete(userRole);
            }
            List<Guid> addIDs = roleIDs.Except(userRoleIDs).ToList();
            AddUserRoles(user, addIDs);
        }
        #endregion
    }
}
