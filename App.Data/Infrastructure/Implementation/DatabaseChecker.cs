namespace App.Data.Infrastructure.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using Microsoft.EntityFrameworkCore.Storage;

    public sealed class DatabaseChecker
    {
        public DatabaseExistenceState DatabaseExists(DbContext context)
        {
            try
            {
                var isExist = (context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists();

                if (isExist)
                {
                    return DatabaseExistenceState.Exists;
                }
                else
                {
                    return DatabaseExistenceState.DoesNotExist;
                }
            }
            catch
            {
                return DatabaseExistenceState.DoesNotExist;
            }
        }
    }

    public enum DatabaseExistenceState
    {
        Unknown,
        DoesNotExist,
        ExistsConsideredEmpty,
        Exists
    }
}
