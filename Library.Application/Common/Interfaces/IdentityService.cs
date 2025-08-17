using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.Common.Interfaces
{

    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);
        Task<bool> AuthorizeAsync(string userId, string policyName);
    }
}
