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
      Emails = new List<string>();
      PhoneNumbers = new List<string>();
      DataRefreshRateInSec = 2;
    }

    public double LowerAlarmBorder { get; set; }
    public double LowerWarnBorder { get; set; }
    public double UpperWarnBorder { get; set; }
    public double UpperAlarmBorder { get; set; }

    public List<string> Emails { get; set; }
    public List<string> PhoneNumbers { get; set; }

    public double DataRefreshRateInSec { get; set; }

    public static Config InstanceConfig
    {
      get
      {
        if (_instanceConfig == null)
          _instanceConfig = new Config();
        return _instanceConfig;
      }
    }

    public void SaveConfig(ConfigViewModel configViewModel)
    {
      var json = JsonConvert.SerializeObject(configViewModel);
      File.WriteAllText(Path.Combine(Startup.Environment.WebRootPath, "configs", "config.json"), json);
    }


    public void RestoreDefaults()
    {
      var configViewModel = (ConfigViewModel) JsonConvert.DeserializeObject(
        File.ReadAllText(Path.Combine(Startup.Environment.WebRootPath, "configs", "default.json")));
      LowerAlarmBorder = configViewModel.LowerAlarmBorder;
      LowerWarnBorder = configViewModel.LowerWarnBorder;
      UpperWarnBorder = configViewModel.UpperWarnBorder;
      UpperAlarmBorder = configViewModel.UpperAlarmBorder;
      Emails = configViewModel.Emails;
      PhoneNumbers = configViewModel.PhoneNumbers;
      DataRefreshRateInSec = configViewModel.DataRefreshRateInSec;
    }

    public ConfigViewModel GenenerateViewModel()
    {
      return new ConfigViewModel
      {
        LowerAlarmBorder = LowerAlarmBorder,
        LowerWarnBorder = LowerWarnBorder,
        UpperWarnBorder = UpperWarnBorder,
        UpperAlarmBorder = UpperAlarmBorder,
        Emails = Emails,
        PhoneNumbers = PhoneNumbers,
        DataRefreshRateInSec = DataRefreshRateInSec
      };
    }

    private void GetConfigFromJson()
    {

    }

    public void SaveConfigFromViewModel(ConfigViewModel configViewModel)
    {
      LowerAlarmBorder = configViewModel.LowerAlarmBorder;
      LowerWarnBorder = configViewModel.LowerWarnBorder;
      UpperWarnBorder = configViewModel.UpperWarnBorder;
      UpperAlarmBorder = configViewModel.UpperAlarmBorder;
      Emails = configViewModel.Emails;
      PhoneNumbers = configViewModel.PhoneNumbers;
      DataRefreshRateInSec = configViewModel.DataRefreshRateInSec;
      SaveConfig(configViewModel);
    }
  }
}