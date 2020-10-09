using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Materal.TTA.EFRepository
{
    public abstract class EFUnitOfWorkImpl<T> : IEFUnitOfWork<T> where T: DbContext
    {
        private T _dbContext;
        protected EFUnitOfWorkImpl(T context)
        {
            _dbContext = context;
        }
        public virtual void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dbContext == null) return;
            _dbContext.Dispose();
            _dbContext = null;
        }
        public virtual void RegisterAdd<TEntity>(TEntity obj) where TEntity : class
        {
            _dbContext.Set<TEntity>().Add(obj);
        }
        public virtual void RegisterEdit<TEntity>(TEntity obj) where TEntity : class
        {
            _dbContext.Entry(obj).State = EntityState.Modified;
        }
        public virtual void RegisterDelete<TEntity>(TEntity obj) where TEntity : class
        {
            _dbContext.Entry(obj).State = EntityState.Deleted;
        }
        public void Commit()
        {
            try
            {
                _dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        } public virtual async Task CommitAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException ex)
            {
                ex.Entries.Single().Reload();
            }
        }
    }
}
