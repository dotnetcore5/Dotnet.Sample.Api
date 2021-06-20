using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dotnet.Sample.Api.Domain.ViewModels
{
    public class AdminAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UserAccount
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
