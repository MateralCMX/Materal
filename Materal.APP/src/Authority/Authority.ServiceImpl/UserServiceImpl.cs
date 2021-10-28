using Authority.Common;
using Authority.DataTransmitModel.User;
using Authority.Domain;
using Authority.Domain.Repositories;
using Authority.Services;
using Authority.Services.Models.User;
using Authority.SqliteEFRepository;
using AutoMapper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Authority.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IAuthoritySqliteEFUnitOfWork _authoritySqliteEFUnitOfWork;
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IMapper mapper, IAuthoritySqliteEFUnitOfWork authoritySqliteEFUnitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _authoritySqliteEFUnitOfWork = authoritySqliteEFUnitOfWork;
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(AddUserModel model)
        {
            if (await _userRepository.ExistedAsync(m => m.Account.Equals(model.Account))) throw new AuthorityException("账号已存在");
            var user = _mapper.Map<User>(model);
            user.ID = Guid.NewGuid();
            user.Password = AuthorityConfig.EncodePassword(AuthorityConfig.DefaultPassword);
            _authoritySqliteEFUnitOfWork.RegisterAdd(user);
            await _authoritySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task EditUserAsync(EditUserModel model)
        {
            if (await _userRepository.ExistedAsync(m =>m.ID != model.ID && m.Account.Equals(model.Account))) throw new AuthorityException("账号已存在");
            User userFromDB = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDB == null) throw new AuthorityException("用户不存在");
            userFromDB = _mapper.Map(model, userFromDB);
            _authoritySqliteEFUnitOfWork.RegisterEdit(userFromDB);
            await _authoritySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new AuthorityException("用户不存在");
            _authoritySqliteEFUnitOfWork.RegisterDelete(userFromDB);
            await _authoritySqliteEFUnitOfWork.CommitAsync();
        }

        public async Task<UserDTO> GetUserInfoAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new AuthorityException("用户不存在");
            var result = _mapper.Map<UserDTO>(userFromDB);
            return result;
        }

        public async Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel model)
        {
            (List<User> usersFromDB, PageModel pageModel) = await _userRepository.PagingAsync(model);
            var result = _mapper.Map<List<UserListDTO>>(usersFromDB);
            return (result, pageModel);
        }

        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account));
            if (userFromDB == null) throw new AuthorityException("账号错误");
            if (!userFromDB.Password.Equals(AuthorityConfig.EncodePassword(model.Password))) throw new AuthorityException("密码错误");
            var result = _mapper.Map<UserDTO>(userFromDB);
            return result;
        }

        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new AuthorityException("用户不存在");
            string password = AuthorityConfig.DefaultPassword;
            userFromDB.Password = AuthorityConfig.EncodePassword(password);
            _authoritySqliteEFUnitOfWork.RegisterEdit(userFromDB);
            await _authoritySqliteEFUnitOfWork.CommitAsync();
            return password;
        }

        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDB == null) throw new AuthorityException("用户不存在");
            if (!userFromDB.Password.Equals(AuthorityConfig.EncodePassword(model.OldPassword))) throw new AuthorityException("旧密码错误");
            userFromDB.Password = AuthorityConfig.EncodePassword(model.NewPassword);
            _authoritySqliteEFUnitOfWork.RegisterEdit(userFromDB);
            await _authoritySqliteEFUnitOfWork.CommitAsync();
        }
    }
}
