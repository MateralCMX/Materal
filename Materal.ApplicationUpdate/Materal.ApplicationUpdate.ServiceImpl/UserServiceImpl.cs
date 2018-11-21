using Materal.ApplicationUpdate.Service;
using System;
using System.Threading.Tasks;
using Materal.ApplicationUpdate.Domain;
using Materal.ApplicationUpdate.Domain.IRepositorys;
using Materal.ApplicationUpdate.DTO.User;

namespace Materal.ApplicationUpdate.ServiceImpl
{
    public class UserServiceImpl : IUserService
    {
        private IUserRepository _userRepository;

        public UserServiceImpl(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<LoginUserDTO> Login(string account, string password)
        {
            User userFormDB = await _userRepository.FirstOrDefaultAsync(m => m.Account.Equals(account));
            if (userFormDB.Password == password)
            {

            }
        }

        public async Task<bool> IsLogin(string token)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginUserDTO> GetUserInfoByToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}
