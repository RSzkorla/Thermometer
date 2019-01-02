using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Hosting;

namespace Thermometer.BLL
{
  public static class Engine
  {
    public static Config Config = Config.InstanceConfig;
    public static List<ISensory> Sensors;
    public static List<List<double>> RecentReadings;
    public static ViewAlerter ViewAlerter;
    public static GsmAlerter GsmAlerter;
    public static MonitorService MonitorService;

    static Engine()
    {
      Sensors = new List<ISensory>
      {
        new ProxySensor(),
        new ProxySensor()
      };

      ViewAlerter = new ViewAlerter();
      GsmAlerter = new ProxyAlerter();

      MonitorService = new MonitorService();

      RecentReadings = new List<List<double>>
      {
        new List<double>
        {
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature()
        },
        new List<double>
        {
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature()
        }
      };
     // MonitorService.StartAsync(new CancellationToken());
    }

    public static bool CheckWarningRange(double temperature)
    {
      if (temperature < Config.LowerWarnBorder || temperature > Config.UpperWarnBorder) return true;
      return false;
    }

    public static bool CheckAlarmRange(double temperature)
    {
      if (temperature < Config.LowerAlarmBorder || temperature > Config.UpperAlarmBorder) return true;
      return false;
    }

    public static string GetRecentReadings()
    {
     
      return RecentReadings.GetDoubleValuesInOneStringFromTwoDimList();
    }

    public static string Monitoring()
    {
      double average = 0, sum = 0;
      for (var i = 0; i < Sensors.Count; i++)
      {
        var sensor = Sensors[i];
        var reading = sensor.GetTemperature();
        sum += reading;
        RecentReadings.ElementAt(i).PushToList(reading);
      }

      average = sum / Sensors.Count;

      if (!CheckWarningRange(average) && !CheckAlarmRange(average))
      {
        ViewAlerter.CanISendAlert = true;
        ViewAlerter.CanISendWarning = true;
        GsmAlerter.CanISendAlert = true;
      }
      if (CheckWarningRange(average) && !CheckAlarmRange(average))
      {
        ViewAlerter.SendWarning("Warning");
        ViewAlerter.CanISendWarning = false;
      }
      if (CheckAlarmRange(average))
      {
        ViewAlerter.SendAlert("ALERT");
        GsmAlerter.SendAlert("Alert!!! Temperature is out of allowed range!");
        ViewAlerter.CanISendAlert = false;
        GsmAlerter.CanISendAlert = false;
      }
      return null;
    }
  }
}