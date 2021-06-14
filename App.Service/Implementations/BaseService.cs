using System;

namespace App.Service
{
    public abstract class BaseService
    {
        protected readonly Guid _userUid;
        protected readonly Guid _productUid;
        protected readonly Guid _customerUid;

        private readonly ICallerService _callerService;

        public BaseService(
            ICallerService callerService
            )
        {
            _callerService = callerService;
            _userUid = _callerService.UserUid;
            _productUid = _callerService.ProductUid;
            _customerUid = _callerService.CustomerUid;
        }
    }
}
