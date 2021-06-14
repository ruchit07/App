using Microsoft.EntityFrameworkCore;

namespace App.Data.Infrastructure.Infrastructure
{
    public static class DbInitializer<T> where T : class
    {
        public static void Initialize(T context)
        {
            var exists = new DatabaseChecker().DatabaseExists(context as DbContext);

            if (exists == DatabaseExistenceState.Exists)
            {
                try
                {
                    (context as DbContext).Database.EnsureCreated();
                }
                catch
                {
                    //This exception will be thrown if the model has changed
                    //if the context model has changed, then run migrations
                    //runAppMigrations(context);
                }
            }
            else
            {
                (context as DbContext).Database.EnsureCreated();
            }
        }
    }
}
