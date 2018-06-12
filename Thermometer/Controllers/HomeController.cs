﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thermometer.BLL;
using Thermometer.Models;

namespace Thermometer.Controllers
{
  public class HomeController : Controller
  {
    public IActionResult Index()
    {
      ViewBag.RefreshRate = Engine.Config.DataRefreshRateInSec;
      return View();
    }

    public IActionResult About()
    {
      ViewData["Message"] = "Your application description page.";

      return View();
    }

    public IActionResult Contact()
    {
      ViewData["Message"] = "Your contact page.";

      return View();
    }

    public IActionResult Error()
    {
      return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }

    public IActionResult Config(string message = "")
    {
      ViewBag.SavedConfigMessage = message;
      return View(Engine.Config.GenenerateViewModel());
    }

    [HttpPost]
    public IActionResult Config(ConfigViewModel config)
    {
      Engine.Config.GetConfigFromViewModel(config);
      return RedirectToAction("Config", "Home", "OK");
    }
  }
}