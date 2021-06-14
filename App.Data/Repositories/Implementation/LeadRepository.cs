using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Models;
using App.Data.Infrastructure;

namespace App.Data.Repositories
{
    public class LeadRepository : AppRepository<Lead>, ILeadRepository
    {
        public LeadRepository(
            IDbFactory<Context.AppContext> dbFactory,
            IUnitOfWork<Context.AppContext> unitOfWork)
            : base(dbFactory, unitOfWork)
        {

        }

        public async Task<IQueryable<Lead>> QueryLead(Guid userUid, Guid productUid, Guid customerUid)
        {
            return await Task.Run(() =>
            {
                return Task.FromResult(dbContext.Lead
                                         .Include(x => x.Addresses)
                                         .Include(x => x.LeadSource)
                                         .Include(x => x.Notes)
                                         .Where(x => !x.IsDeleted
                                           && x.IsActive
                                           && x.UserUid.Equals(userUid)
                                           && x.CustomerUid.Equals(customerUid)
                                           && x.ProductUid.Equals(productUid)));
            });
        }
    }
}
    