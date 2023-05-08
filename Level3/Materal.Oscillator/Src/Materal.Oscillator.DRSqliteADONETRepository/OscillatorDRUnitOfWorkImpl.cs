using Materal.Oscillator.DR;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.Oscillator.DRSqliteADONETRepository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class OscillatorDRUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<OscillatorDRDBOption, Guid>, IOscillatorDRUnitOfWork
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        public OscillatorDRUnitOfWorkImpl(IServiceProvider serviceProvider, OscillatorDRDBOption dbOption) : base(serviceProvider, dbOption)
        {
        }
    }
}
