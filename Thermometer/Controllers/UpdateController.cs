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
    private static bool _firstLoadFailed = true;
    public IActionResult Index(string guid)
    {
      string readGuid = System.IO.File.ReadAllText("guid");
      if (readGuid != guid&&_firstLoadFailed) return RedirectToAction("Error");
      //_firstLoadFailed = false;
      ViewBag.RefreshRate = Engine.Config.DataRefreshRateInSec;
      ViewBag.CollectionRate = Engine.Config.DataCollectionRateInSec;
      ViewBag.ReportTime = Engine.Config.ReportTime;
      ViewBag.SessionGuid = guid;
      return View(Engine.Config.GenenerateViewModel());
    }

    public IActionResult Error()
    {
      return View();
    }
  }
}