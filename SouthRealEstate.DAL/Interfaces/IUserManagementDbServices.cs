using SouthRealEstate.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SouthRealEstate.DAL.Interfaces
{
    public interface IUserManagementDbServices
    {
        Task<UmUsers> AuthenticateUserAsync(string userName, string password);
    }
}
