using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thermometer.BLL
{
  public class Monitor
  {
    public List<List<double>> RecentReadings;

    public bool CheckWarningRange(double temperature)
    {
      if (temperature < Engine.Config.LowerWarnBorder || temperature > Engine.Config.UpperWarnBorder) return true;
      return false;
    }

    public bool CheckAlarmRange(double temperature)
    {
      if (temperature < Engine.Config.LowerAlarmBorder || temperature > Engine.Config.UpperAlarmBorder) return true;
      return false;
    }

    public string GetRecentReadings()
    {
      return RecentReadings.GetDoubleValuesInOneStringFromTwoDimList();
    }

    private void DoStuff()
    {
      while (true)
      {
        double average = 0, sum = 0;
        for (var i = 0; i < Engine.Sensors.Count; i++)
        {
          var sensor = Engine.Sensors[i];
          var reading = sensor.GetTemperature();
          sum += reading;
          RecentReadings.ElementAt(i).PushToList(reading);
        }

        average = sum / Engine.Sensors.Count;

        if (!CheckWarningRange(average) && !CheckAlarmRange(average))
        {
          Engine.ViewAlerter.CanISendAlert = true;
          Engine.ViewAlerter.CanISendWarning = true;
          Engine.GsmAlerter.CanISendAlert = true;
        }
        if (CheckWarningRange(average) && !CheckAlarmRange(average))
        {
          Engine.ViewAlerter.SendWarning("Warning");
          Engine.ViewAlerter.CanISendWarning = false;
        }
        if (CheckAlarmRange(average))
        {
          Engine.ViewAlerter.SendAlert("ALERT");
          Engine.GsmAlerter.SendAlert("Alert!!! Temperature is out of allowed range!");
          Engine.ViewAlerter.CanISendAlert = false;
          Engine.GsmAlerter.CanISendAlert = false;
        }
        Task.Delay(100);
      }
      
    }

    public async Task Monitoring()
    {
      await Task.Factory.StartNew(DoStuff);
    }
  }
}