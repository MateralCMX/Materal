using AutoMapper;
using Materal.ConfigCenter.ProtalServer.DataTransmitModel.User;
using Materal.ConfigCenter.ProtalServer.Domain;
using Materal.ConfigCenter.ProtalServer.Domain.Repositories;
using Materal.ConfigCenter.ProtalServer.PresentationModel.User;
using Materal.ConfigCenter.ProtalServer.Services;
using Materal.ConfigCenter.ProtalServer.SqliteEFRepository;
using Materal.ConvertHelper;
using Materal.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Materal.ConfigCenter.ProtalServer.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IProtalServerUnitOfWork _protalServerUnitOfWork;
        private readonly IMapper _mapper;

        public UserServiceImpl(IUserRepository userRepository, IProtalServerUnitOfWork protalServerUnitOfWork, IMapper mapper)
        {
            _userRepository = userRepository;
            _protalServerUnitOfWork = protalServerUnitOfWork;
            _mapper = mapper;
        }

        public async Task AddUserAsync(AddUserModel model)
        {
            if (await _userRepository.ExistedAsync(m => m.Account.Equals(model.Account))) throw new MateralConfigCenterException("账号已存在");
            var user = model.CopyProperties<User>();
            user.Password = string.IsNullOrEmpty(model.Password) ? "123456".ToMd5() : user.Password.ToMd5();
            _protalServerUnitOfWork.RegisterAdd(user);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task EditUserAsync(EditUserModel model)
        {
            if (await _userRepository.ExistedAsync(m => m.Account.Equals(model.Account) && m.ID != model.ID)) throw new MateralConfigCenterException("账号已存在");
            User userFromDb = await _userRepository.FirstOrDefaultAsync(model.ID);
            if (userFromDb == null) throw new MateralConfigCenterException("用户不存在");
            string oldPassword = userFromDb.Password;
            model.CopyProperties(userFromDb);
            userFromDb.Password = string.IsNullOrEmpty(model.Password) ? oldPassword : userFromDb.Password.ToMd5();
            userFromDb.UpdateTime = DateTime.Now;
            _protalServerUnitOfWork.RegisterEdit(userFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task DeleteUserAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            User userFromDb = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDb == null) throw new MateralConfigCenterException("用户不存在");
            _protalServerUnitOfWork.RegisterDelete(userFromDb);
            await _protalServerUnitOfWork.CommitAsync();
        }

        public async Task<UserDTO> GetUserInfoAsync([Required(ErrorMessage = "唯一标识不能为空")]Guid id)
        {
            User userFromDb = await _userRepository.FirstOrDefaultAsync(id);
            if (userFromDb == null) throw new MateralConfigCenterException("用户不存在");
            var result = _mapper.Map<UserDTO>(userFromDb);
            return result;
        }

        public async Task<(List<UserListDTO> result, PageModel pageModel)> GetUserListAsync(QueryUserFilterModel filterModel)
        {
            (List<User> usersFromDb, PageModel pageModel) = await _userRepository.PagingAsync(filterModel);
            var result = _mapper.Map<List<UserListDTO>>(usersFromDb);
            return (result, pageModel);
        }

        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User userFromDb = await _userRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account));
            if (userFromDb == null) throw new MateralConfigCenterException("用户名或密码错误");
            model.Password = model.Password.ToMd5();
            if (!userFromDb.Password.Equals(model.Password)) throw new MateralConfigCenterException("用户名或密码错误");
            var result = _mapper.Map<UserDTO>(userFromDb);
            return result;
        }
    }
}
