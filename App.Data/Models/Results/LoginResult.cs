using System;

namespace App.Data.Models.Results
{
    public class LoginResult
    {
        public string UserName { get; set; }
        public Guid Uid { get; set; }
        public Guid ProductUid { get; set; }
        public Guid CustomerUid { get; set; }
        public string Role { get; set; }
        public string OriginalUserName { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string RedirectUri { get; set; }
    }
}
