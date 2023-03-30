using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Materal.TTA.Demo
{
    public interface IDemoRepository<T, TPrimaryKeyType> : IEFRepository<T, TPrimaryKeyType, TTADemoDBContext>
        where T : class, IEntity<TPrimaryKeyType>
        where TPrimaryKeyType : struct
    {
    }
}
