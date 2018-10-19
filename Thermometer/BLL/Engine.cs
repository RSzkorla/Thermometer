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
    public static ViewAlerter ViewAlerter;
    public static GsmAlerter GsmAlerter;
    public static Monitor Monitor;

     static Engine()
    {
      Sensors = new List<ISensory>()
      {
        new ProxySensor(),
        new ProxySensor()
      };

      ViewAlerter = new ViewAlerter();
      GsmAlerter = new GsmAlerter();

      Monitor.RecentReadings = new List<List<double>>
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
        Monitor.Monitoring();
    }

    
  }
}