using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Demo.ConsoleApp.EFRepositories
{
    public class UserUnitOfWorkImpl : EFUnitOfWorkImpl<UserDbContext>, IUserUnitOfWork
    {
        public UserUnitOfWorkImpl(UserDbContext context) : base(context)
        {
        }
    }
}
