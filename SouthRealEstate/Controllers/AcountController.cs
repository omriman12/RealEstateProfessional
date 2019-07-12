using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SouthRealEstate.DTOs;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Models;

namespace SouthRealEstate.Controllers
{
    [Route("api/acount/")]
    [AllowAnonymous]
    public class AcountController : Controller
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(AcountController));

        private readonly IUserManagementLogic m_UserManagementLogic;

        public AcountController(IUserManagementLogic userManagementLogic)
        {
            m_UserManagementLogic = userManagementLogic;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginDTO loginDTO)
        {
            ActionResult retVal = null;

            try
            {
                bool didAuthenticate = await m_UserManagementLogic.AuthenticateUserAsync(HttpContext, loginDTO.UserName, loginDTO.Password);

                if (didAuthenticate)
                {
                    retVal = Ok();
                }
                else
                {
                    retVal = Unauthorized();
                }
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("logout")]
        [HttpPost]
        public async Task<IActionResult> LogoutAsync()
        {
            ActionResult retVal = null;

            try
            {
                await m_UserManagementLogic.SignOutUserAsync(HttpContext);

                retVal = RedirectToAction("Home", "Index");
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

    }
}
