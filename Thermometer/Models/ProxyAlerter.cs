using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Thermometer.BLL;

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
      Engine.UpdateHub.SendAlert(DateTime.Now.ToShortTimeString() + " "+ message);
    }
  }
}