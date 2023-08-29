using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Materal.BaseCore.EFRepository
{
    /// <summary>
    /// 核心工作单元
    /// </summary>
    public interface IMateralCoreUnitOfWork : IEFUnitOfWork<Guid>, IEFUnitOfWork, IUnitOfWork<Guid>, IUnitOfWork
    {
    }
}
