using System.Threading.Tasks;
using App.Data.Models;

namespace App.Service
{
    public interface ILeadService : IVmAppService<Lead, LeadVm, LeadFilter, LeadResult>
    {
        
    }
}
