using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Thermometer.Controllers
{
    public class UpdateController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}