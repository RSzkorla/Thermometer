using System;

namespace Thermometer.BLL
{
  public class TimeStamp
  {
    public DateTime Time { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }

    public TimeStamp(string type,string value)
    {
      Time = DateTime.Now;
      Type = type;
      Value = value;
    }

    public override string ToString()
    {
      return Time.ToShortDateString() + " "
             + Time.ToShortTimeString() + " "
             + Type + ":"
             + Value;
    }
  }
}