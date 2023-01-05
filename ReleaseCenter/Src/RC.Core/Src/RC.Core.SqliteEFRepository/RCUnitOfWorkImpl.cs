using Materal.TTA.Common;
using Materal.TTA.SqliteRepository;
using Microsoft.EntityFrameworkCore;
using RC.Core.SqliteEFRepository;

namespace RC.Core.SqlServer
{
    public class RCUnitOfWorkImpl<T> : SqliteEFUnitOfWorkImpl<T>, IRCUnitOfWork, IUnitOfWork
        where T : DbContext
    {
        public RCUnitOfWorkImpl(T context) : base(context)
        {
        }

        public void RegisterAdd<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterAdd<TEntity, Guid>(obj);
        }

        public void RegisterDelete<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterDelete<TEntity, Guid>(obj);
        }

        public void RegisterEdit<TEntity>(TEntity obj)
            where TEntity : class, IEntity<Guid>
        {
            RegisterEdit<TEntity, Guid>(obj);
        }
    }
}
