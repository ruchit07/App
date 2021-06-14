using System;

namespace App.Data.Infrastructure
{
    public interface IDbFactory<T> : IDisposable where T : class
    {
        T Init();
    }
}
