using Materal.Oscillator.Abstractions.Repositories;
using Materal.TTA.SqliteADONETRepository;

namespace Materal.Oscillator.SqliteADONETRepository
{
    /// <summary>
    /// 工作单元
    /// </summary>
    public class OscillatorUnitOfWorkImpl : SqliteADONETUnitOfWorkImpl<OscillatorDBOption, Guid>, IOscillatorUnitOfWork
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="dbOption"></param>
        public OscillatorUnitOfWorkImpl(IServiceProvider serviceProvider, OscillatorDBOption dbOption) : base(serviceProvider, dbOption)
        {
        }
    }
}
