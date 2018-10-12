using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thermometer.BLL;

namespace Thermometer.Models
{
  public class ConfigViewModel
  {
    public double LowerAlarmBorder { get; set; }
    public double LowerWarnBorder { get; set; }
    public double UpperWarnBorder { get; set; }
    public double UpperAlarmBorder { get; set; }

    public double CountOfSensors = Engine.Sensors.Count;

    public List<string> Emails { get; set; }
    public List<string> PhoneNumbers { get; set; }
    public double DataRefreshRateInSec { get; set; }
  }
}
