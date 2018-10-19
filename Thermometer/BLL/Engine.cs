using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Thermometer.Hubs;
using Thermometer.Models;

namespace Thermometer.BLL
{
  public static class Engine
  {
    public static Config Config = Config.InstanceConfig;
    public static List<ISensory> Sensors;
    public static List<List<double>> RecentReadings;
    public static ViewAlerter ViewAlerter;
    public static GsmAlerter GsmAlerter;
    public static Monitor Monitor;

     static Engine()
    {
      Sensors = new List<ISensory>()
      {
        new Esp32Sensor(),
        new Esp32Sensor()
      };

      ViewAlerter = new ViewAlerter();
      GsmAlerter = new GsmAlerter();
      Monitor = new Monitor();

      RecentReadings = new List<List<double>>
      {
        new List<double>()
        {
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature()
        },
        new List<double>()
        {
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature()
        }
      };
        
    }

    public static bool CheckWarningRange(double temperature)
    {
      if (temperature < Engine.Config.LowerWarnBorder || temperature > Engine.Config.UpperWarnBorder) return true;
      return false;
    }

    public static bool CheckAlarmRange(double temperature)
    {
      if (temperature < Engine.Config.LowerAlarmBorder || temperature > Engine.Config.UpperAlarmBorder) return true;
      return false;
    }

    public static string GetRecentReadings()
    {
      Monitoring();
      return RecentReadings.GetDoubleValuesInOneStringFromTwoDimList();
    }

    public static void Monitoring()
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
    }
    
  }
}