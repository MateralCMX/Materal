using RC.Authority.Common;
using RC.Authority.DataTransmitModel.User;
using RC.Authority.Domain;
using RC.Authority.Services.Models.User;
using RC.Core.Common;

namespace RC.Authority.ServiceImpl
{
    public partial class UserServiceImpl
    {
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<Guid> AddAsync(AddUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Account == model.Account)) throw new RCException("账号已存在");
            return await base.AddAsync(model);
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="domain"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        protected override async Task<Guid> AddAsync(User domain, AddUserModel model)
        {
            string password = ApplicationConfig.DefaultPassword;
            domain.Password = ApplicationConfig.EncodePassword(password);
            return await base.AddAsync(domain, model);
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task EditAsync(EditUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.Account == model.Account)) throw new RCException("账号已存在");
            await base.EditAsync(model);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User? domain = await DefaultRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account));
            if (domain == null) throw new RCException("账号错误");
            if (!domain.Password.Equals(ApplicationConfig.EncodePassword(model.Password))) throw new RCException("密码错误");
            var result = Mapper.Map<UserDTO>(domain);
            return result;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User? domain = await DefaultRepository.FirstOrDefaultAsync(id);
            if (domain == null) throw new RCException("用户不存在");
            string password = ApplicationConfig.DefaultPassword;
            domain.Password = ApplicationConfig.EncodePassword(password);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
            return password;
        }
        /// <summary>
        /// 更改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            User? domain = await DefaultRepository.FirstOrDefaultAsync(model.ID);
            if (domain == null) throw new RCException("用户不存在");
            if (!domain.Password.Equals(ApplicationConfig.EncodePassword(model.OldPassword))) throw new RCException("旧密码错误");
            domain.Password = ApplicationConfig.EncodePassword(model.NewPassword);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        /// <summary>
        /// 添加默认用户
        /// </summary>
        /// <returns></returns>
        /// <exception cref="RCException"></exception>
        public async Task AddDefaultUserAsync()
        {
            if (await DefaultRepository.ExistedAsync(m => true)) return;
            await AddAsync(new AddUserModel
            {
                Account = "Admin",
                Name = "管理员"
            });
        }
    }
}
