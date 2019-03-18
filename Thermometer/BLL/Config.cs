using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Thermometer.Models;

namespace Thermometer.BLL
{
  public class Config
  {
    private static Config _instanceConfig;

    private Config()
    {
      //var configViewModel = JsonConvert.DeserializeObject(
      //  File.ReadAllText(Path.Combine(Startup.Environment.WebRootPath, "configs", "config.json"))) as ConfigViewModel;
      //LowerAlarmBorder = configViewModel.LowerAlarmBorder;
      //LowerWarnBorder = configViewModel.LowerWarnBorder;
      //UpperWarnBorder = configViewModel.UpperWarnBorder;
      //UpperAlarmBorder = configViewModel.UpperAlarmBorder;
      //Emails = configViewModel.Emails;
      //PhoneNumbers = configViewModel.PhoneNumbers;
      //DataRefreshRateInSec = configViewModel.DataRefreshRateInSec;
      LowerAlarmBorder = 10;
      LowerWarnBorder = 11 ;
      UpperWarnBorder = 15 ;
      UpperAlarmBorder = 16;
      PhoneNumbers = new List<string>();
      DataRefreshRateInSec = 2;
      DataCollectionRateInSec = 60;
      ReportTime = DateTime.Parse("11:48:00");
    }

    public double LowerAlarmBorder { get; set; }
    public double LowerWarnBorder { get; set; }
    public double UpperWarnBorder { get; set; }
    public double UpperAlarmBorder { get; set; }


    public List<string> PhoneNumbers { get; set; }

    public double DataRefreshRateInSec { get; set; }
    public double DataCollectionRateInSec { get; set; }
    public DateTime ReportTime { get; set; }

    public static Config InstanceConfig
    {
      get
      {
        if (_instanceConfig == null)
          _instanceConfig = new Config();
        return _instanceConfig;
      }
    }

    public void SaveConfigToFile(ConfigViewModel configViewModel)
    {
      var json = JsonConvert.SerializeObject(configViewModel);
      File.WriteAllText("config.json", json);
    }


    public void RestoreDefaults()
    {
      var configViewModel = (ConfigViewModel) JsonConvert.DeserializeObject(
        File.ReadAllText(Path.Combine(Startup.Environment.WebRootPath, "configs", "default.json")));
      LowerAlarmBorder = configViewModel.LowerAlarmBorder;
      LowerWarnBorder = configViewModel.LowerWarnBorder;
      UpperWarnBorder = configViewModel.UpperWarnBorder;
      UpperAlarmBorder = configViewModel.UpperAlarmBorder;

      PhoneNumbers = configViewModel.PhoneNumbers;
      DataRefreshRateInSec = configViewModel.DataRefreshRateInSec;
      ReportTime = configViewModel.ReportTime;
    }

   
    private void GetConfigFromJson()
    {

    }

    public ConfigViewModel GenenerateViewModel()
    {
      var cvm = new ConfigViewModel();
      cvm.CountOfSensors = Engine.Sensors.Count;
      cvm.LowerAlarmBorder =LowerAlarmBorder;
      cvm.LowerWarnBorder = LowerWarnBorder;
      cvm.UpperWarnBorder = UpperWarnBorder;
      cvm.UpperAlarmBorder =UpperAlarmBorder;
      return cvm;
    }

    public string GetBorderValuesInOneString()
    {
      return " LA: " + LowerAlarmBorder +
             " LW: " + LowerWarnBorder +
             " UW: " + UpperWarnBorder +
             " UA: " + UpperAlarmBorder;

    }
  }         
}           