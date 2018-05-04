using System.Collections.Generic;

namespace Thermometer.Models
{
  public class Config
  {
    private static Config _instanceConfig;

    public double LowerAlarmBorder { get; set; }
    public double LoverWarnBorder { get; set; }
    public double UpperWarnBorder { get; set; }
    public double UpperAlarmBorder { get; set; }

    public List<string> Emails { get; set; }
    public List<string> PhoneNumbers { get; set; }
    private Config()
    {
      LowerAlarmBorder = 10;
      LoverWarnBorder = 11;
      UpperWarnBorder = 20;
      UpperAlarmBorder = 22;
      Emails = new List<string>(){"r.szkorla@gmail.com"};
      PhoneNumbers =new List<string>();
    }

    public void SaveConfig()
    {
      
    }


    public void RestoreDefaults()
    {
      LowerAlarmBorder = 10;
      LoverWarnBorder = 11;
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

  }
}