using Microsoft.AspNetCore.Mvc;
using App.Data.Models;
using App.Service;
using App.Data.Models.Results;

namespace App.Api.Controllers.App
{
    [Produces("application/json")]
    [Route("api/lead")]
    public class LeadController : BaseVmController<Lead, LeadVm, LeadFilter, Result>
    {
        private readonly IVmAppService<Lead, LeadVm, LeadFilter, Result> _leadService;

        public LeadController(
            IVmAppService<Lead, LeadVm, LeadFilter, Result> leadService,
            ICallerService callerService) 
            : base(leadService, callerService)
        {
            _leadService = leadService;
        }
    }
}
