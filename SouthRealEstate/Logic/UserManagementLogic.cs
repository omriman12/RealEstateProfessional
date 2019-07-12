using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using SouthRealEstate.DAL.Entities;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.Helpers.Security;
using SouthRealEstate.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SouthRealEstate.Logic
{
    public class UserManagementLogic : IUserManagementLogic
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(UserManagementLogic));

        private readonly IUserManagementDbServices m_UserManagementDbServices;
        private readonly IEncryptionService m_EncryptionService;
        

        public UserManagementLogic(IUserManagementDbServices userManagementDbServices
            , IEncryptionService encryptionService)
        {
            m_UserManagementDbServices = userManagementDbServices;
            m_EncryptionService = encryptionService;
        }

        public async Task<bool> AuthenticateUserAsync(HttpContext httpContext, string userName, string password)
        {
            bool retVal = false;
            
            try
            {
                string passwordEnc;
                m_EncryptionService.Encrypt(password, out passwordEnc);
                var user = await m_UserManagementDbServices.AuthenticateUserAsync(userName, passwordEnc);
                if (user != null)
                {
                    s_Logger.Debug($"user:{userName} has successfully authenticated");
                    var claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name, userName),
                        new Claim(ClaimTypes.Role, "Administrator"),
                    };

                    var claimsIdentity = new ClaimsIdentity(
                        claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = false,
                        ExpiresUtc = DateTime.Now.AddMinutes(20),
                    };

                    await httpContext.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity),
                        authProperties);

                    retVal = true;
                }
                else
                {
                    s_Logger.Debug($"user:{userName} falied to authenticate");
                    retVal = false;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return retVal;
        }

        public async Task SignOutUserAsync(HttpContext httpContext)
        {

            try
            {
                await httpContext.SignOutAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme); 
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
