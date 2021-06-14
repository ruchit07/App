namespace App.Data.Infrastructure
{
    using Microsoft.EntityFrameworkCore.Storage;
    using System;
    using System.Threading.Tasks;

    public class UnitOfWork<T> : IUnitOfWork<T> where T : class
    {
        private readonly IDbFactory<T> dbFactory;
        private T dbContext;
        private IDbContextTransaction _transaction;

        public UnitOfWork(IDbFactory<T> dbFactory)
        {
            this.dbFactory = dbFactory;
        }

        public dynamic DbContext
        {
            get { return dbContext ?? (dbContext = dbFactory.Init()); }
        }

        public async Task BeginTransaction()
        {
            await Task.Run(() =>
            {
                _transaction = DbContext.Database.BeginTransaction();
            });
        }

        public async Task Rollback()
        {
            await Task.Run(() =>
            {
                if (_transaction != null)
                {
                    _transaction.Rollback();
                    _transaction.Dispose();
                }
            });
        }

        public async Task CommitAsync()
        {
            try
            {
                await DbContext.SaveChangesAsync();
                if (_transaction != null)
                {
                    _transaction.Commit();
                }
                    
            }
            catch (Exception)
            {
                if (_transaction != null)
                    _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
            }
        }
    }
}
