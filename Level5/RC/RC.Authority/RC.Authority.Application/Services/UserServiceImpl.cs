using Microsoft.Extensions.Options;
using RC.Authority.Abstractions.DTO.User;
using RC.Authority.Abstractions.Services.Models.User;

namespace RC.Authority.Application.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public partial class UserServiceImpl(IOptionsMonitor<ApplicationConfig> config)
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
            string password = config.CurrentValue.DefaultPassword;
            domain.Password = EncodePassword(password);
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
            User domain = await DefaultRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account)) ?? throw new RCException("账号错误");
            if (!domain.Password.Equals(EncodePassword(model.Password))) throw new RCException("密码错误");
            UserDTO result = Mapper.Map<UserDTO>(domain) ?? throw new RCException("映射失败"); ;
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
            User domain = await DefaultRepository.FirstOrDefaultAsync(id) ?? throw new RCException("用户不存在");
            string password = config.CurrentValue.DefaultPassword;
            domain.Password = EncodePassword(password);
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
            User domain = await DefaultRepository.FirstOrDefaultAsync(model.ID) ?? throw new RCException("用户不存在");
            if (!domain.Password.Equals(EncodePassword(model.OldPassword))) throw new RCException("旧密码错误");
            domain.Password = EncodePassword(model.NewPassword);
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
        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        private static string EncodePassword(string inputString) => $"Materal{inputString}Materal".ToMd5_32Encode();
    }
}
