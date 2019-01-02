using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thermometer.BLL;

namespace Thermometer.Controllers
{
  public class UpdateController : Controller
  {
    public IActionResult Index(string message)
    {
      ViewBag.RefreshRate = Engine.Config.DataRefreshRateInSec;
      ViewBag.ConfigResult = message;
      return View(Engine.Config.GenenerateViewModel());
    }
  }
}