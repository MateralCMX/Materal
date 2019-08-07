using Materal.TTA.Common;
using System;

namespace Demo.Domain.Repositories
{
    /// <summary>
    /// 班级仓储
    /// </summary>
    public interface IClassRepository : IEFSubordinateRepository<Class, Guid>
    {
    }
}
