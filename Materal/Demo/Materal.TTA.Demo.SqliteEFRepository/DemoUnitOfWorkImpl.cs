using Materal.TTA.Demo.Domain;
using Materal.TTA.EFRepository;

namespace Materal.TTA.Demo.SqliteEFRepository
{
    public class DemoUnitOfWorkImpl : EFUnitOfWorkImpl<TTADemoDBContext>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(TTADemoDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
