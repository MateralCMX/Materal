using Materal.Oscillator.Abstractions;
using Materal.Oscillator.Abstractions.Domain;

namespace Materal.Oscillator.SqlServerEFRepository
{
    /// <summary>
    /// Oscillator工作单元
    /// </summary>
    public class OscillatorUnitOfWorkImpl(OscillatorDBContext context, IServiceProvider serviceProvider) : SqlServerEFUnitOfWorkImpl<OscillatorDBContext>(context, serviceProvider), IOscillatorUnitOfWork
    {
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
        /// 注册修改 
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
