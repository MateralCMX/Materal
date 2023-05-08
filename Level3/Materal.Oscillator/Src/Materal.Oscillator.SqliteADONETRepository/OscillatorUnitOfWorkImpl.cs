using Materal.Oscillator.Abstractions.Domain;
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
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public override void RegisterAdd<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if (obj is IDomain domain)
            {
                domain.CreateTime = DateTime.Now;
            }
            base.RegisterAdd<TEntity, TPrimaryKeyType>(obj);
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public override void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if (obj is IDomain domain)
            {
                domain.UpdateTime = DateTime.Now;
            }
            base.RegisterEdit<TEntity, TPrimaryKeyType>(obj);
        }
    }
}
