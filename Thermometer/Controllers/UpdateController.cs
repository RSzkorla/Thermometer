using Microsoft.AspNetCore.Mvc;
using Thermometer.BLL;

namespace Thermometer.Controllers
{
  public class UpdateController : Controller
  {
    private static readonly bool _firstLoadFailed = true;

    public IActionResult Index(string guid)
    {
      var readGuid = System.IO.File.ReadAllText("guid");
      if (readGuid != guid && _firstLoadFailed) return RedirectToAction("Error");
      //_firstLoadFailed = false;
      ViewBag.RefreshRate = 2;
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