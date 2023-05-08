using Materal.Oscillator.Abstractions.Domain;
using Materal.Oscillator.Abstractions.Repositories;
using Materal.TTA.Common;
using Materal.TTA.SqlServerEFRepository;

namespace Materal.Oscillator.SqlServerEFRepository
{
    /// <summary>
    /// Oscillator工作单元
    /// </summary>
    public class OscillatorUnitOfWorkImpl : SqlServerEFUnitOfWorkImpl<OscillatorDBContext, Guid>, IOscillatorUnitOfWork
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceProvider"></param>
        public OscillatorUnitOfWorkImpl(OscillatorDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
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
        /// 注册修改 
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TPrimaryKeyType"></typeparam>
        /// <param name="obj"></param>
        public override void RegisterEdit<TEntity, TPrimaryKeyType>(TEntity obj)
        {
            if(obj is IDomain domain)
            {
                domain.UpdateTime = DateTime.Now;
            }
            base.RegisterEdit<TEntity, TPrimaryKeyType>(obj);
        }
    }
}
