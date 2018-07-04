using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Thermometer.BLL
{
    public class GsmAlerter:IAlerter
    {

    private IHostingEnvironment _env;
    public bool CanISendAlert = true;
    public List<string> PhoneNumberList { get; set; }

    public GsmAlerter()
    {
      _env = Startup.Environment;
      PhoneNumberList = new List<string>()
      {
        "+48883984162"
      };
    }
    public void SendWarning(string message)
    {
      
    }

    public void SendAlert(string message)
    {
      if (CanISendAlert)
      {
        foreach (var number in PhoneNumberList)
        {
          var script = File.ReadAllText(Path.Combine(_env.WebRootPath, "bashScripts", "smsTemp.txt"));
          script.Replace("{phoneNumber}", number);
          script.Replace("{message}", message);
          File.WriteAllText(Path.Combine(_env.WebRootPath, "bashScripts", "script.sh"), script);
          ExecuteScript(Path.Combine(".",_env.WebRootPath, "bashScripts", "script.sh"));
        }
      }
    }

    private string ExecuteScript(string cmd)
    {

      var process = new Process()
      {
        StartInfo = new ProcessStartInfo
        {
          FileName = "/bin/bash",
          RedirectStandardOutput = true,
          UseShellExecute = false,
          CreateNoWindow = true,
        }
      };
      process.Start();
      string result = process.StandardOutput.ReadToEnd();
      process.WaitForExit();
      return result;
    }
  }
}
