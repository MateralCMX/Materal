using Materal.TTA.Common;
using Materal.TTA.EFRepository;

namespace Materal.TTA.Demo
{
    public class DemoUnitOfWorkImpl : EFUnitOfWorkImpl<TTADemoDBContext>, IDemoUnitOfWork
    {
        public DemoUnitOfWorkImpl(TTADemoDBContext context, IServiceProvider serviceProvider) : base(context, serviceProvider)
        {
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterAdd<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册添加
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public bool TryRegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            return TryRegisterAdd<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterEdit<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册修改
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public bool TryRegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            return TryRegisterEdit<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public void RegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterDelete<TEntity, Guid>(obj);
        }
        /// <summary>
        /// 注册删除
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="obj"></param>
        public bool TryRegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            return TryRegisterDelete<TEntity, Guid>(obj);
        }
    }
}
