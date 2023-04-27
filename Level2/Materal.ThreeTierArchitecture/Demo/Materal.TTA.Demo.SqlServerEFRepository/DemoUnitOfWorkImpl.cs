using Materal.TTA.Demo.Domain;
using Materal.TTA.SqlServerEFRepository;

namespace Materal.TTA.Demo.SqlServerEFRepository
{
    public class DemoUnitOfWorkImpl : SqlServerEFUnitOfWorkImpl<TTADemoDBContext, Guid>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(TTADemoDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
    }
}
