﻿using System;
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

    public static List<double> RecentReadings;

    static Engine()
    {
      Sensors = new List<ISensory>()
      {
        new GpioSensor()
      };
      //Alerters = new List<IAlerter>()
      //{
      //  new ProxyAlerter(Startup.Environment)
      //};
      ViewAlerter = new ViewAlerter();

      RecentReadings = new List<double>()
      {
        Convert.ToDouble(Sensors[0].GetTemperatureAsync()),
        Convert.ToDouble(Sensors[0].GetTemperatureAsync()),
        Convert.ToDouble(Sensors[0].GetTemperatureAsync()),
        Convert.ToDouble(Sensors[0].GetTemperatureAsync()),
        Convert.ToDouble(Sensors[0].GetTemperatureAsync()),
      };
      RecentReadings.Reverse();
      var test1 = RecentReadings.GetValuesInOneString();
      RecentReadings.PushToList(0);
      var test2 = RecentReadings;

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
        double reading = sensor.GetTemperatureAsync();
        sum += reading;
        strb.Append(reading).Append("\n");
      }

      average = sum / Sensors.Count;
      RecentReadings.PushToList(average);
      if (!CheckWarningRange(average)&&!CheckAlarmRange(average))
      {
        ViewAlerter.CanISendAlert = true;
        ViewAlerter.CanISendWarning = true;
      }
      if (CheckWarningRange(average)&&!CheckAlarmRange(average))
      {
        ViewAlerter.SendWarning("Warning");
        ViewAlerter.CanISendWarning = false;
      }
      if (CheckAlarmRange(average))
      {
        ViewAlerter.SendAlert("ALERT");
        ViewAlerter.CanISendAlert = false;
      }
      return RecentReadings.GetValuesInOneString();
    }

  }
}