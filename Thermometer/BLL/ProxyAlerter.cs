﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Thermometer.BLL;

namespace Thermometer.BLL
{
  public class ProxyAlerter : IAlerter
  {
    private IHostingEnvironment _env;

    public ProxyAlerter(IHostingEnvironment env)
    {
      _env = env;
    }


    public void SendWarning(string message)
    {
      string content = File.ReadAllText(Path.Combine(_env.WebRootPath, "temp", "proxyWarnings.txt"));
      var strb = new StringBuilder();
      strb.Append(content);
      strb.AppendLine(message);
      File.WriteAllText(Path.Combine(_env.WebRootPath, "temp", "proxyWarnings.txt"), strb.ToString());
    }

    public void SendAlert(string message)
    {
      string content = File.ReadAllText(Path.Combine(_env.WebRootPath, "temp", "proxyAlerts.txt"));
      var strb = new StringBuilder();
      strb.Append(content);
      strb.AppendLine(message);
      File.WriteAllText(Path.Combine(_env.WebRootPath, "temp", "proxyAlerts.txt"), strb.ToString());
    }
  }
}