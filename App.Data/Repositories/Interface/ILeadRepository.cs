using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Models;

namespace App.Data.Repositories
{
    public interface ILeadRepository : IAppRepository<Lead>
    {
        Task<IQueryable<Lead>> QueryLead(Guid userUid, Guid productUid, Guid customerUid);
    }
}
