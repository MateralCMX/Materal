using Materal.TTA.Common;
using Microsoft.EntityFrameworkCore;

namespace Materal.TTA.EFRepository
{
    /// <summary>
    /// 迁移帮助类
    /// </summary>
    public interface IMigrateHelper<TDBContext> : IMigrateHelper
        where TDBContext : DbContext
    {
    }
}
