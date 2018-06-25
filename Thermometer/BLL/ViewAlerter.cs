using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thermometer.Hubs;

namespace Thermometer.Models
{
    public class ViewAlerter:IAlerter
    {
      public bool CanISendWarning = true;
      public bool CanISendAlert = true;
      public UpdateHub Hub;

      public void SendWarning(string message)
      {
       if (CanISendWarning) Hub.SendWarning(DateTime.Now.ToLocalTime() + " "+ message);
      }

      public void SendAlert(string message)
      {
        if (CanISendAlert) Hub.SendAlert(DateTime.Now.ToLocalTime() + " " + message);
      }
    }
}
