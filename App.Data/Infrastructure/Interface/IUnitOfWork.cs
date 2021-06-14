namespace App.Data.Infrastructure
{
    using System.Threading.Tasks;

    public interface IUnitOfWork<T> where T : class
    {
        Task CommitAsync();
        Task BeginTransaction();
        Task Rollback();
    }
}