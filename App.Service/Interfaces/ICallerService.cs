using System;

namespace App.Service
{
    public interface ICallerService
    {
        public Guid UserUid { get; }
        public Guid ProductUid { get; }
        public Guid CustomerUid { get; }
        public string Role { get; }
    }
}
