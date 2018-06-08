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
    public static List<IAlerter> Alerters;
    public static UpdateHub UpdateHub;

    static Engine()
    {
      Sensors = new List<ISensory>()
      {
        new GpioSensor()
      };
      Alerters = new List<IAlerter>()
      {
        new ProxyAlerter(Startup.Environment)
      };
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

    public static string GetReadings()
    {
      var strb = new StringBuilder();
      double average = 0,sum=0;
      foreach (var sensor in Sensors)
      {
        double reading = sensor.GetTemperature();
        sum += reading;
        strb.Append(reading).Append("\n");
      }
 
      average = sum / Sensors.Count;
      if (CheckWarningRange(average)&&!CheckAlarmRange(average))
      {
        UpdateHub.SendWarning(DateTime.Now.ToLocalTime() + " Warning");
      }
      if (CheckAlarmRange(average))
      {
        UpdateHub.SendAlert(DateTime.Now.ToLocalTime() + " Alarm");
      }
      return average +"\n" + strb.ToString();
    }

  }
}