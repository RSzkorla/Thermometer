using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Thermometer.BLL;
using Thermometer.Models;

namespace Thermometer.Controllers
{
  public class HomeController : Controller
  {
    
    public IActionResult Index(string message)
    {
      ViewBag.RefreshRate = Engine.Config.DataRefreshRateInSec;
      ViewBag.ConfigResult = message;
      return View(Engine.Config.GenenerateViewModel());
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

    public IActionResult Config()
    {
      return View();
    }

    [HttpPost]
    public IActionResult Config(ConfigViewModel config)
    {
      
      return RedirectToAction("Index", "Home",new { message = "Ok" });
    }

    public IActionResult Reports()
    {
      var files = Directory.GetFiles(Path.Combine(Path.Combine(Environment.CurrentDirectory, "Reports"))).ToList();
      
      return View(files.Select(Path.GetFileName).Reverse().ToList());
    }

    public async Task<IActionResult> GetFile(string fileName)
    {
      if (fileName == null)
        return Content("FileNotFound");

      var path = Path.Combine(
        Directory.GetCurrentDirectory(),
        "Reports", fileName);

      var memory = new MemoryStream();
      using (var stream = new FileStream(path, FileMode.Open))
      {
        await stream.CopyToAsync(memory);
      }
      memory.Position = 0;
      return File(memory, GetContentType(path), Path.GetFileName(path));
    }

    private string GetContentType(string path)
    {
      var types = GetMimeTypes();
      var ext = Path.GetExtension(path).ToLowerInvariant();
      return types[ext];
    }

    private Dictionary<string, string> GetMimeTypes()
    {
      return new Dictionary<string, string>
      {
        {".txt", "text/plain"},
        {".pdf", "application/pdf"},
        {".doc", "application/vnd.ms-word"},
        {".docx", "application/vnd.ms-word"},
        {".xls", "application/vnd.ms-excel"},
        {".xlsx", "application/vnd.openxmlformatsofficedocument.spreadsheetml.sheet"},  
          {".png", "image/png"},
        {".jpg", "image/jpeg"},
        {".jpeg", "image/jpeg"},
        {".gif", "image/gif"},
        {".csv", "text/csv"}
      };
      }

  }
}