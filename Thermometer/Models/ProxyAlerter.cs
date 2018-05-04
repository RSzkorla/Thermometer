using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;

namespace Thermometer.Models
{
  public class ProxyAlerter : IAlerter
  {
    private IHostingEnvironment _env;

    public ProxyAlerter(IHostingEnvironment env)
    {
      _env = env;
    }

    public void SendAlert(string message)
    {
      var path = Path.Combine(_env.WebRootPath, (DateTime.Now.ToShortTimeString() + ".txt"));
      var sw = File.CreateText(path);
      sw.WriteLine(message);
      sw.Dispose();
    }
  }
}