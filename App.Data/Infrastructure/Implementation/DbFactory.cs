using System;

namespace App.Data.Infrastructure
{
    public class DbFactory<T> : Disposable, IDbFactory<T> where T : class
    {
        T dbContext;

        public T Init()
        {
            return dbContext ?? (dbContext = (T)Activator.CreateInstance(typeof(T)));
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                ((dynamic)dbContext).Dispose();
            }
        }
    }
}
