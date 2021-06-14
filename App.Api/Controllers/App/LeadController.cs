using Microsoft.AspNetCore.Mvc;
using App.Data.Models;
using App.Service;

namespace App.Api.Controllers.App
{
    [Produces("application/json")]
    [Route("api/lead")]
    public class LeadController : BaseVmController<Lead, LeadVm, LeadFilter, LeadResult>
    {
        private readonly IVmAppService<Lead, LeadVm, LeadFilter, LeadResult> _leadService;

        public LeadController(
            IVmAppService<Lead, LeadVm, LeadFilter, LeadResult> leadService,
            ICallerService callerService) 
            : base(leadService, callerService)
        {
            _leadService = leadService;
        }
    }
}
