using Materal.TTA.Common;

namespace Demo.ConsoleApp.EFRepositories
{
    public class UserUnitOfWorkImpl : EFUnitOfWorkImpl<UserDbContext>, IUserUnitOfWork
    {
        public UserUnitOfWorkImpl(UserDbContext context) : base(context)
        {
        }
    }
}
