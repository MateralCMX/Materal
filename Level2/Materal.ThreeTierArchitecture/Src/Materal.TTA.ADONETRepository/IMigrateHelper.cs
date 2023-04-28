using Materal.TTA.Common;

namespace Materal.TTA.ADONETRepository
{
    /// <summary>
    /// 迁移帮助类
    /// </summary>
    public interface IMigrateHelper<TDBOption> : IMigrateHelper
        where TDBOption : DBOption
    {
    }
}
