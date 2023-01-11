using RC.Core.Common;
using RC.Demo.Common;
using RC.Demo.DataTransmitModel.User;
using RC.Demo.Domain;
using RC.Demo.Services.Models.User;

namespace RC.Demo.ServiceImpl
{
    public partial class UserServiceImpl
    {
        public override async Task<Guid> AddAsync(AddUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Account == model.Account)) throw new RCException("账号已存在");
            return await base.AddAsync(model);
        }
        protected override async Task<Guid> AddAsync(User domain, AddUserModel model)
        {
            string password = DemoConfig.DefaultPassword;
            domain.Password = DemoConfig.EncodePassword(password);
            return await base.AddAsync(domain, model);
        }
        public override async Task EditAsync(EditUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.Account == model.Account)) throw new RCException("账号已存在");
            await base.EditAsync(model);
        }
        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User? domain = await DefaultRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account));
            if (domain == null) throw new RCException("账号错误");
            if (!domain.Password.Equals(DemoConfig.EncodePassword(model.Password))) throw new RCException("密码错误");
            UserDTO result = Mapper.Map<UserDTO>(domain);
            return result;
        }
        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User? domain = await DefaultRepository.FirstOrDefaultAsync(id);
            if (domain == null) throw new RCException("用户不存在");
            string password = DemoConfig.DefaultPassword;
            domain.Password = DemoConfig.EncodePassword(password);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
            return password;
        }
        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            User? domain = await DefaultRepository.FirstOrDefaultAsync(model.ID);
            if (domain == null) throw new RCException("用户不存在");
            if (!domain.Password.Equals(DemoConfig.EncodePassword(model.OldPassword))) throw new RCException("旧密码错误");
            domain.Password = DemoConfig.EncodePassword(model.NewPassword);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
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
