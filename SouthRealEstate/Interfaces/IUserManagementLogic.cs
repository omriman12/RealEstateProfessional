using Microsoft.AspNetCore.Http;
using SouthRealEstate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SouthRealEstate.Interfaces
{
    public interface IUserManagementLogic
    {
        Task<bool> AuthenticateUserAsync(HttpContext httpContext, string userName, string password);
        Task SignOutUserAsync(HttpContext httpContext);
    }
}
