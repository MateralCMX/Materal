using AutoMapper;
using Materal.ConDep.Center.Common;
using Materal.ConDep.Center.DataTransmitModel.User;
using Materal.ConDep.Center.Domain;
using Materal.ConDep.Center.Domain.Repositories;
using Materal.ConDep.Center.Services;
using Materal.ConDep.Center.Services.Models.User;
using Materal.ConDep.Center.SqliteEFRepository;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Materal.ConDep.Center.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IMapper _mapper;
        private readonly ICenterSqliteEFUnitOfWork _centerSqliteEFUnitOfWork;
        private readonly IUserRepository _userRepository;

        public UserServiceImpl(IMapper mapper, ICenterSqliteEFUnitOfWork centerSqliteEFUnitOfWork, IUserRepository userRepository)
        {
            _mapper = mapper;
            _centerSqliteEFUnitOfWork = centerSqliteEFUnitOfWork;
            _userRepository = userRepository;
        }

        public async Task AddUserAsync(AddUserModel model)
        {
            if (await _userRepository.ExistedAsync(m => m.Account.Equals(model.Account))) throw new MateralConDepException("账号已存在");
            var user = _mapper.Map<User>(model);
            user.ID = Guid.NewGuid();
            user.Password = ApplicationConfig.EncodePassword(ApplicationConfig.DefaultPassword);
            _centerSqliteEFUnitOfWork.RegisterAdd(user);
            await _centerSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task EditUserAsync(EditUserModel model)
        {
            if (await _userRepository.ExistedAsync(m =>m.ID != model.ID && m.Account.Equals(model.Account))) throw new MateralConDepException("账号已存在");
            User userFromDB = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDB == null) throw new MateralConDepException("用户不存在");
            userFromDB = _mapper.Map(model, userFromDB);
            _centerSqliteEFUnitOfWork.RegisterEdit(userFromDB);
            await _centerSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new MateralConDepException("用户不存在");
            _centerSqliteEFUnitOfWork.RegisterDelete(userFromDB);
            await _centerSqliteEFUnitOfWork.CommitAsync();
        }

        public async Task<UserDTO> GetUserInfoAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new MateralConDepException("用户不存在");
            var result = _mapper.Map<UserDTO>(userFromDB);
            return result;
        }

        public async Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel)
        {
            (List<User> usersFromDB, PageModel pageModel) = await _userRepository.PagingAsync(filterModel);
            var result = _mapper.Map<List<UserListDTO>>(usersFromDB);
            return (result, pageModel);
        }

        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account));
            if (userFromDB == null) throw new MateralConDepException("账号错误");
            if (!userFromDB.Password.Equals(ApplicationConfig.EncodePassword(model.Password))) throw new MateralConDepException("密码错误");
            var result = _mapper.Map<UserDTO>(userFromDB);
            return result;
        }

        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDB == null) throw new MateralConDepException("用户不存在");
            string password = ApplicationConfig.DefaultPassword;
            userFromDB.Password = ApplicationConfig.EncodePassword(password);
            _centerSqliteEFUnitOfWork.RegisterEdit(userFromDB);
            await _centerSqliteEFUnitOfWork.CommitAsync();
            return password;
        }

        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            User userFromDB = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDB == null) throw new MateralConDepException("用户不存在");
            if (!userFromDB.Password.Equals(ApplicationConfig.EncodePassword(model.OldPassword))) throw new MateralConDepException("旧密码错误");
            userFromDB.Password = ApplicationConfig.EncodePassword(model.NewPassword);
            _centerSqliteEFUnitOfWork.RegisterEdit(userFromDB);
            await _centerSqliteEFUnitOfWork.CommitAsync();
        }
    }
}
