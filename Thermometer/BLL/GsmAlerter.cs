using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
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
        var strb = new StringBuilder();
        foreach (var number in PhoneNumberList)
        {
          //  strb.AppendLine("#!/usr/bin/expect -f");
          //  strb.AppendLine("spawn minicom -D /dev/ttyS0\\r\"");
          //  strb.AppendLine("send \"AT\\r\"");
          //  strb.AppendLine("expect");
          //  strb.AppendLine("send \"AT + CMGF = 1\\r\"");
          //  strb.AppendLine("expect");
          //  strb.AppendLine($"send \"AT+CMGS=\\\"{number}\\\"\\r\"");
          //  strb.AppendLine("expect");
          //  strb.AppendLine($"send \"{message}\r\"");
          //  strb.AppendLine("expect");
          //  strb.AppendLine("send \"\032\"");
          //  File.WriteAllText(Path.Combine(_env.WebRootPath, "bashScripts", "script.sh"), strb.ToString());
          //  var path = Path.Combine(_env.WebRootPath, "bashScripts", "script.sh");
          var path = Path.Combine("/home", "pi", "script.sh");
        path.Bash();
        }
      }
    }
  }
}
