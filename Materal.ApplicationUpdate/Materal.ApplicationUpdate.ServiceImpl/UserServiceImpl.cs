using AutoMapper;
using Materal.ApplicationUpdate.Common;
using Materal.ApplicationUpdate.Domain;
using Materal.ApplicationUpdate.Domain.IRepositorys;
using Materal.ApplicationUpdate.DTO.User;
using Materal.ApplicationUpdate.Service;
using Materal.ApplicationUpdate.Service.Model.User;
using Materal.CacheHelper;
using Materal.Common;
using Materal.DateTimeHelper;
using Materal.LinqHelper;
using Materal.StringHelper;
using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Materal.ApplicationUpdate.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICacheHelper _cacheHelper;
        private readonly IUnitOfWork _unitOfWork;

        public UserServiceImpl(IUserRepository userRepository, IMapper mapper, ICacheHelper cacheHelper, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _cacheHelper = cacheHelper;
            _unitOfWork = unitOfWork;
        }

        public async Task AddUserAsync(AddUserModel addUserModel)
        {
            if (addUserModel == null) throw new ArgumentNullException(nameof(addUserModel));
            if (string.IsNullOrEmpty(addUserModel.Name)) throw new ArgumentNullException(nameof(addUserModel.Name), "用户名称不能为空。");
            if (string.IsNullOrEmpty(addUserModel.Account)) throw new ArgumentNullException(nameof(addUserModel.Account), "用户账户不能为空。");
            var user = new User
            {
                Account = addUserModel.Account,
                Name = addUserModel.Name,
                Password = EncryptionManager.GetMd5_32(ApplicationConfig.DefaultPassword)
            };
            _unitOfWork.RegisterAdd(user);
            await _unitOfWork.CommitAsync();
        }

        public async Task EditUserAsync(EditUserModel editUserModel)
        {
            if (editUserModel == null) throw new ArgumentNullException(nameof(editUserModel));
            if (string.IsNullOrEmpty(editUserModel.Name)) throw new ArgumentNullException(nameof(editUserModel.Name), "用户名称不能为空");
            User userFromDB = await RetrievalUserByIDAsync(editUserModel.ID);
            userFromDB.Name = editUserModel.Name;
            _unitOfWork.RegisterEdit(userFromDB);
            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync(Guid userID)
        {
            User userFromDB = await RetrievalUserByIDAsync(userID);
            _unitOfWork.RegisterDelete(userFromDB);
            await _unitOfWork.CommitAsync();
        }

        public async Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(UserListFilter userListFilter)
        {
            if (userListFilter == null) throw new ArgumentNullException(nameof(userListFilter));
            Expression<Func<User, bool>> expression = m => true;
            if (!string.IsNullOrEmpty(userListFilter.Account))
            {
                expression = expression.And(m => m.Account.Equals(userListFilter.Account));
            }
            if (!string.IsNullOrEmpty(userListFilter.Name))
            {
                expression = expression.And(m => EF.Functions.Like(m.Account, $"%{userListFilter.Account}%"));
            }
            (List<User> resultFromDB, PageModel pageModel) = await _userRepository.PagingAsync(expression, userListFilter);
            var result = _mapper.Map<List<UserListDTO>>(resultFromDB);
            return (result, pageModel);
        }

        public async Task<UserDTO> GetUserInfoByIDAsync(Guid userID)
        {
            User userFromDB = await RetrievalUserByIDAsync(userID);
            var result = _mapper.Map<UserDTO>(userFromDB);
            return result;
        }

        public async Task<LoginUserDTO> LoginAsync(UserLoginModel userLoginModel)
        {
            if (userLoginModel == null) throw new ArgumentNullException(nameof(userLoginModel));
            if (string.IsNullOrEmpty(userLoginModel.Account)) throw new ArgumentNullException(nameof(userLoginModel.Account), "账户不能为空。");
            if (string.IsNullOrEmpty(userLoginModel.Password)) throw new ArgumentNullException(nameof(userLoginModel.Password), "密码不能为空。");
            if (userLoginModel.Password.Length != 32) throw new ArgumentException("密码未加密。", nameof(userLoginModel.Password));
            User userFromDB = await _userRepository.FirstOrDefaultAsync(m => m.Account.Equals(userLoginModel.Account));
            if (userFromDB == null) throw new ApplicationUpdateException("用户名错误");
            if (!userFromDB.Password.Equals(userLoginModel.Password)) throw new ApplicationUpdateException("密码错误");
            _cacheHelper.Remove(userFromDB.Token);
            do
            {
                userFromDB.Token = StringManager.GetRandomStrByGuid();
            } while (await RetrievalUserByTokenAsync(userFromDB.Token) != null);
            userFromDB.TokenExpireDate = DateTime.Now.AddSeconds(ApplicationConfig.TokenEffectiveSecond);
            _unitOfWork.RegisterEdit(userFromDB);
            await _unitOfWork.CommitAsync();
            var result = _mapper.Map<LoginUserDTO>(userFromDB);
            _cacheHelper.SetBySliding(result.Token, result, 1);
            return result;
        }

        public async Task<LoginUserDTO> GetUserInfoByTokenAsync(string token)
        {
            var result = _cacheHelper.Get<LoginUserDTO>(token);
            if (result != null) return result;
            User userFromDB = await _userRepository.FirstOrDefaultAsync(m => m.Token.Equals(token) && m.TokenExpireDate > DateTime.Now);
            if (userFromDB == null) return null;
            result = _mapper.Map<LoginUserDTO>(userFromDB);
            _cacheHelper.SetBySliding(result.Token, result, 1);
            return result;
        }

        public async Task ChangePasswordAsync(ChangePasswordModel changePasswordModel)
        {
            if (changePasswordModel == null) throw new ArgumentNullException(nameof(changePasswordModel));
            if (string.IsNullOrEmpty(changePasswordModel.OldPassword)) throw new ArgumentNullException(nameof(changePasswordModel.OldPassword), "旧密码不能为空");
            if (string.IsNullOrEmpty(changePasswordModel.NewPassword)) throw new ArgumentNullException(nameof(changePasswordModel.NewPassword), "新密码不能为空");
            if (changePasswordModel.OldPassword.Length != 32) throw new ArgumentException("旧密码未加密。", nameof(changePasswordModel.OldPassword));
            if (changePasswordModel.NewPassword.Length != 32) throw new ArgumentException("新密码未加密。", nameof(changePasswordModel.NewPassword));
            if (changePasswordModel.OldPassword.Equals(changePasswordModel.NewPassword)) throw new ArgumentException("新密码不能与旧密码相同", nameof(changePasswordModel.NewPassword));
            User userFromDB = await RetrievalUserByIDAsync(changePasswordModel.UserID);
            if(!userFromDB.Password.Equals(changePasswordModel.OldPassword)) throw new ApplicationUpdateException("旧密码错误");
            userFromDB.Password = changePasswordModel.NewPassword;
            _unitOfWork.RegisterEdit(userFromDB);
            await _unitOfWork.CommitAsync();
        }

        #region 私有方法
        /// <summary>
        /// 根据唯一标识检索用户
        /// </summary>
        /// <param name="userID">用户唯一标识</param>
        /// <returns></returns>
        /// <exception cref="ApplicationUpdateException"></exception>
        private async Task<User> RetrievalUserByIDAsync(Guid userID)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(userID);
            if (userFromDB == null) throw new ApplicationUpdateException("无此唯一标识用户。");
            return userFromDB;
        }
        /// <summary>
        /// 根据Token检索用户
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        private async Task<User> RetrievalUserByTokenAsync(string token)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(m => m.Token.Equals(token));
            return userFromDB;
        }
        #endregion
    }
}
