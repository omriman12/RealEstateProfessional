using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SouthRealEstate.Models;

namespace SouthRealEstate.Controllers
{
    public class PropertiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleProperty()
        {
            return View();
        }
    }
}
