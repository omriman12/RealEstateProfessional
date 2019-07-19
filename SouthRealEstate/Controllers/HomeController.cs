using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SouthRealEstate.Models;

namespace SouthRealEstate.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog s_Logger = log4net.LogManager.GetLogger(typeof(HomeController));
        public IActionResult Index()
        {
            s_Logger.Info("starting home page");
            return View();
        }
        
        public IActionResult Contact()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult About()
        {
            //ViewData["Message"] = "Your contact page.";

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
