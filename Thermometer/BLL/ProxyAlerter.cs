using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Thermometer.BLL
{
  public class ProxyAlerter : GsmAlerter, IAlerter
  {
    private readonly IHostingEnvironment _env;

    public ProxyAlerter()
    {
      _env = Startup.Environment;
    }


    public new void SendWarning(string message)
    {
      var content = File.ReadAllText(Path.Combine(_env.WebRootPath, "temp", "proxyWarnings.txt"));
      var strb = new StringBuilder();
      strb.Append(content);
      strb.AppendLine(message);
      File.WriteAllText(Path.Combine(_env.WebRootPath, "temp", "proxyWarnings.txt"), strb.ToString());
    }

    public new void SendAlert(string message)
    {
      var content = File.ReadAllText(Path.Combine(_env.WebRootPath, "temp", "proxyAlerts.txt"));
      var strb = new StringBuilder();
      strb.Append(content);
      strb.AppendLine(message);
      File.WriteAllText(Path.Combine(_env.WebRootPath, "temp", "proxyAlerts.txt"), strb.ToString());
    }
  }
}