using Materal.Utils.Model;
using MBC.Core.Common;
using MBC.Demo.Common;
using MBC.Demo.DataTransmitModel.User;
using MBC.Demo.Domain;
using MBC.Demo.Services.Models.User;

namespace MBC.Demo.ServiceImpl
{
    public partial class UserServiceImpl
    {
        public async Task<(List<UserListDTO> data, PageModel pageInfo)> GetUserListAsync(QueryUserModel model)
        {
            return await GetListAsync(model);
        }
        public override async Task<Guid> AddAsync(AddUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.Account == model.Account)) throw new MBCException("账号已存在");
            return await base.AddAsync(model);
        }
        protected override async Task<Guid> AddAsync(User domain, AddUserModel model)
        {
            string password = ApplicationConfig.DefaultPassword;
            domain.Password = ApplicationConfig.EncodePassword(password);
            return await base.AddAsync(domain, model);
        }
        public override async Task EditAsync(EditUserModel model)
        {
            if (await DefaultRepository.ExistedAsync(m => m.ID != model.ID && m.Account == model.Account)) throw new MBCException("账号已存在");
            await base.EditAsync(model);
        }
        public async Task<UserDTO> LoginAsync(LoginModel model)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(m => m.Account.Equals(model.Account)) ?? throw new MBCException("账号错误");
            if (!domain.Password.Equals(ApplicationConfig.EncodePassword(model.Password))) throw new MBCException("密码错误");
            UserDTO result = Mapper.Map<UserDTO>(domain);
            return result;
        }
        public async Task<string> ResetPasswordAsync(Guid id)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(id) ?? throw new MBCException("用户不存在");
            string password = ApplicationConfig.DefaultPassword;
            domain.Password = ApplicationConfig.EncodePassword(password);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
            return password;
        }
        public async Task TestChangePasswordAsync(ChangePasswordModel model)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(model.ID) ?? throw new MBCException("用户不存在");
            if (!domain.Password.Equals(ApplicationConfig.EncodePassword(model.OldPassword))) throw new MBCException("旧密码错误");
            domain.Password = ApplicationConfig.EncodePassword(model.NewPassword);
            UnitOfWork.RegisterEdit(domain);
            await UnitOfWork.CommitAsync();
        }
        public async Task ChangePasswordAsync(ChangePasswordModel model)
        {
            User domain = await DefaultRepository.FirstOrDefaultAsync(model.ID) ?? throw new MBCException("用户不存在");
            if (!domain.Password.Equals(ApplicationConfig.EncodePassword(model.OldPassword))) throw new MBCException("旧密码错误");
            domain.Password = ApplicationConfig.EncodePassword(model.NewPassword);
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
