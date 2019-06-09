using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SouthRealEstate.DAL.Interfaces;
using SouthRealEstate.Interfaces;
using SouthRealEstate.Models;

namespace SouthRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(PropertiesController));

        private readonly IPropertiesLogic m_PropertiesLogic;

        public PropertiesController(IPropertiesLogic propertiesLogic)
        {
            m_PropertiesLogic = propertiesLogic;
        }
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleProperty()
        {
            return View();
        }

        [Route("api/cities")]
        [HttpGet]
        public async Task<IActionResult> GetAllCities()
        {
            ActionResult retVal = null;

            try
            {
                var cities = await m_PropertiesLogic.GetCityEntitiesAsync();
                retVal = Ok(cities);
            }
            catch (Exception ex)
            {
                s_Logger.Error(ex);
                retVal = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

            return retVal;
        }

        [Route("api/properties/residental")]
        [HttpGet]
        public async Task<IActionResult> GetAllResidentalPropertiesAsync()
        {
            ActionResult retVal = null;

            try
            {
                var cities = await m_PropertiesLogic.GetAllResidentalPropertiesAsync();
                retVal = Ok(cities);
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
