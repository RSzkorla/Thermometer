using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Thermometer.Models;

namespace Thermometer.BLL
{
  public class Config
  {


    public Config()
    {
      
    }

    public double LowerAlarmBorder { get; set; }
    public double LowerWarnBorder { get; set; }
    public double UpperWarnBorder { get; set; }
    public double UpperAlarmBorder { get; set; }


    public List<string> PhoneNumbers { get; set; }

    public double DataRefreshRateInSec { get; set; }
    public double DataCollectionRateInSec { get; set; }
    public DateTime ReportTime { get; set; }



    public void SaveConfigToFile()
    {
      var json = JsonConvert.SerializeObject(this);
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