using System;
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
      throw new NotImplementedException();
    }

    public void SendAlert(string message)
    {
      throw new NotImplementedException();
    }
  }
}