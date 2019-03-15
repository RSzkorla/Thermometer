using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Thermometer.BLL
{
  public class GsmAlerter : IAlerter
  {
    private readonly IHostingEnvironment _env;
    public bool CanISendAlert = true;

    public GsmAlerter()
    {
      _env = Startup.Environment;
      PhoneNumberList = new List<string>
      {
        "+48883984162"
      };
    }

    public List<string> PhoneNumberList { get; set; }

    public void SendWarning(string message)
    {
    }

    public void SendAlert(string message)
    {
      var r = Environment.NewLine;
      if (CanISendAlert)
      {
        var strb = new StringBuilder();
        strb.Append("#!/usr/bin/expect -f" +r+
        "spawn minicom -D /dev/ttyS0\\r\"" + r +
        "send \"AT\\r\"" + r +
        "expect" + r);
        foreach (var number in PhoneNumberList)
        {
          strb.Append("send \"AT + CMGF = 1\\r\"" + r +
          "expect" + r +
          $"send \"AT+CMGS=\\\"{number}\\\"\\r\"" + r +
          "expect" + r +
          $"send \"{message}\r\"" + r +
          "expect" + r +
          "send \"\032\"" + r);
        }
        File.WriteAllText(Path.Combine("/home","pi", "script.sh"), strb.ToString());
        var path = Path.Combine(_env.WebRootPath, "bashScripts", "script.sh");
        //var path = Path.Combine("/home", "pi", "script.sh");
        path.Bash();
      }
    }
  }
}