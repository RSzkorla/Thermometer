using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Thermometer.Models;

namespace Thermometer.BLL
{
  public class Config
  {
    private static Config _instanceConfig;

    public double LowerAlarmBorder { get; set; }
    public double LowerWarnBorder { get; set; }
    public double UpperWarnBorder { get; set; }
    public double UpperAlarmBorder { get; set; }

    public List<string> Emails { get; set; }
    public List<string> PhoneNumbers { get; set; }

    public double DataRefreshRateInSec { get; set; }

    private Config()
    {
      LowerAlarmBorder = 10;
      LowerWarnBorder = 11;
      UpperWarnBorder = 20;
      UpperAlarmBorder = 22;
      DataRefreshRateInSec = 1;
      Emails = new List<string>() {"r.szkorla@gmail.com"};
      PhoneNumbers = new List<string>();
    }

    public void SaveConfig()
    {
      var json = JsonConvert.SerializeObject(this);
      File.WriteAllText(Path.Combine(Startup.Environment.WebRootPath, "configs", "config.json"), json);
    }


    public void RestoreDefaults()
    {
      LowerAlarmBorder = 10;
      LowerWarnBorder = 11;
      UpperWarnBorder = 29;
      UpperAlarmBorder = 30;
    }

    public static Config InstanceConfig
    {
      get
      {
        if (_instanceConfig == null)
          _instanceConfig = new Config();
        return _instanceConfig;
      }
    }

    public ConfigViewModel GenenerateViewModel()
    {
      return new ConfigViewModel()
      {
        LowerAlarmBorder = this.LowerAlarmBorder,
        LowerWarnBorder = this.LowerWarnBorder,
        UpperWarnBorder = this.UpperWarnBorder,
        UpperAlarmBorder = this.UpperAlarmBorder,
        Emails = this.Emails,
        PhoneNumbers = this.PhoneNumbers,
        DataRefreshRateInSec = this.DataRefreshRateInSec
      };
    }

    public void SaveConfigFromViewModel(ConfigViewModel config)
    {
      LowerAlarmBorder = config.LowerAlarmBorder;
      LowerWarnBorder = config.LowerWarnBorder;
      UpperWarnBorder = config.UpperWarnBorder;
      UpperAlarmBorder = config.UpperAlarmBorder;
      Emails = config.Emails;
      PhoneNumbers = config.PhoneNumbers;
      DataRefreshRateInSec = config.DataRefreshRateInSec;
      SaveConfig();
    }
  }
}