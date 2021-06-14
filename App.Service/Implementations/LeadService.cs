using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data.Models;
using App.Data.Repositories;
using App.Service.Extention;

namespace App.Service
{
    public class LeadService : VmAppService<Lead, LeadVm, LeadFilter, LeadResult>, ILeadService
    {
        #region 'Service Initialization'
        private readonly ILeadRepository _leadRepository;
        private readonly IAppRepository<LeadAddress> _addressRepository;
        #endregion

        #region 'Constructor'
        public LeadService(
           ILeadRepository leadRepository,
           ICallerService callerService,
           IErrorLogService errorLogService,
           IAppRepository<LeadAddress> addressRepository
           ) : base(callerService, leadRepository)
        {
            _leadRepository = leadRepository ?? throw new ArgumentNullException(nameof(ILeadRepository));
            _addressRepository = addressRepository;
        }
        #endregion

        #region 'Get'
        public override async Task<IEnumerable<LeadResult>> GetAll(LeadFilter filter)
        {
            IQueryable<Lead> query = (await QueryLead())
                  .Contains(filter)
                  .Filter(filter)
                  .Sort(filter);

            int count = query.Count();

            return query.Paging(filter)
                        .ToList()
                        .ToResult(count);
        }
        #endregion

        #region 'Add'
        public override async Task<LeadResult> AddAsync(LeadVm model)
        {
            model.Validate();
            SetUser(model);

            await _leadRepository.BeginTransaction();
            Lead lead = await _leadRepository.AddWithoutSaveAsync(model.ToEntity());
            await _leadRepository.CommitAsync();

            return lead.ToResult();
        }
        #endregion

        #region 'Update'
        public override async Task<LeadResult> UpdateAsync(LeadVm model, long leadId)
        {
            if (leadId <= 0)
            {
                throw new ArgumentNullException(nameof(leadId));
            }

            model.Validate();
            SetUser(model);

            await _leadRepository.BeginTransaction();
            Lead lead = await GetLeadById(leadId);
            if (lead == null)
            {
                throw new ArgumentNullException(nameof(Lead));
            }
            
            return (await UpdateAsync(model.ToEntity(lead))).ToResult();
        }

        public override async Task<Lead> UpdateAsync(Lead entity)
        {
            entity = await _leadRepository.UpdateWithoutSaveAsync(entity, entity.LeadId);
            await _leadRepository.CommitAsync();
            return entity;
        }
        #endregion`

        #region 'Delete'
        public override async Task<long> DeleteAsync(Lead lead, long leadId)
        {
            return await _leadRepository.DeleteAsync(lead, leadId);
        }
        #endregion

        #region 'Private Functions'
        private async Task<IQueryable<Lead>> QueryLead()
        {
            return await _leadRepository.QueryLead(_userUid, _productUid, _customerUid);
        }

        private async Task<Lead> GetLeadById(long leadId)
        {
            Lead lead = await _leadRepository.GetAsync(leadId);
            if (lead == null)
            {
                throw new KeyNotFoundException(nameof(lead));
            }

            return lead;
        }

        private void SetUser(LeadVm model)
        {
            model.UserUid = _userUid;
            model.CustomerUid = _customerUid;
            model.ProductUid = _productUid;
        }
        #endregion
    }
}
