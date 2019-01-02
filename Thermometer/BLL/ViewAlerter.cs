using System;
using Thermometer.Hubs;

namespace Thermometer.BLL
{
  public class ViewAlerter : IAlerter
  {
    public bool CanISendAlert = true;
    public bool CanISendWarning = true;
    public UpdateHub Hub;

    public void SendWarning(string message)
    {
      if (!CanISendWarning) return;
      Engine.CollectedTimeStamps.Add(new TimeStamp("Warning", message));
      Hub.SendWarning(DateTime.Now.ToLocalTime() + " " + message);
    }

    public void SendAlert(string message)
    {
      if (!CanISendAlert) return;
      Engine.CollectedTimeStamps.Add(new TimeStamp("Alert", message));
      Hub.SendAlert(DateTime.Now.ToLocalTime() + " " + message);
    }
  }
}