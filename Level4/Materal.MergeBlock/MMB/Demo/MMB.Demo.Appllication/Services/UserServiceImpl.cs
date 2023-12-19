using Materal.MergeBlock.Application.Services;
using Microsoft.Extensions.Options;
using MMB.Demo.Abstractions.DTO.User;
using MMB.Demo.Abstractions.Services.Models.User;

namespace MMB.Demo.Appllication.Services
{
    /// <summary>
    /// 用户服务
    /// </summary>
    public class UserServiceImpl(IServiceProvider serviceProvider, IOptionsMonitor<ApplicationConfig> applicationConfig) : BaseServiceImpl<AddUserModel, EditUserModel, QueryUserModel, UserDTO, UserListDTO, IUserRepository, User>(serviceProvider), IUserService
    {
        /// <summary>
        /// 获得用户列表
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<(List<UserListDTO> data, PageModel pageInfo)> GetUserListAsync(QueryUserModel model) => await GetListAsync(model);
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MMBException"></exception>
        public override async Task<Guid> AddAsync(AddUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Account == model.Account)) throw new MMBException("账号已存在");
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
            string password = applicationConfig.CurrentValue.DefaultPassword;
            domain.Password = PasswordManager.EncodePassword(password);
            return await base.AddAsync(domain, model);
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MMBException"></exception>
        public override async Task EditAsync(EditUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.Account == model.Account)) throw new MMBException("账号已存在");
            await base.EditAsync(model);
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MMBException"></exception>
        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account)) ?? throw new MMBException("账号错误");
            if (!domain.Password.Equals(PasswordManager.EncodePassword(model.Password))) throw new MMBException("密码错误");
            UserDTO result = Mapper.Map<UserDTO>(domain);
            return result;
        }
        /// <summary>
        /// 重置密码
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="MMBException"></exception>
        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(id) ?? throw new MMBException("用户不存在");
            string password = applicationConfig.CurrentValue.DefaultPassword;
            domain.Password = PasswordManager.EncodePassword(password);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
            return password;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MMBException"></exception>
        public async Task TestChangePasswordAsync(ChangePasswordModel model)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(model.ID) ?? throw new MMBException("用户不存在");
            if (!domain.Password.Equals(PasswordManager.EncodePassword(model.OldPassword))) throw new MMBException("旧密码错误");
            domain.Password = PasswordManager.EncodePassword(model.NewPassword);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        /// <exception cref="MMBException"></exception>
        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(model.ID) ?? throw new MMBException("用户不存在");
            if (!domain.Password.Equals(PasswordManager.EncodePassword(model.OldPassword))) throw new MMBException("旧密码错误");
            domain.Password = PasswordManager.EncodePassword(model.NewPassword);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        /// <summary>
        /// 添加默认用户
        /// </summary>
        /// <returns></returns>
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
