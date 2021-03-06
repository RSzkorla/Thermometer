﻿using System;

namespace Thermometer.BLL
{
  public class TimeStamp
  {
    public TimeStamp(string type, string value)
    {
      Time = DateTime.Now;
      Type = type;
      Value = value;
    }

    public DateTime Time { get; set; }
    public string Type { get; set; }
    public string Value { get; set; }

    public override string ToString()
    {
      return Time.ToShortDateString() + " "
                                      + Time.ToLongTimeString() + " "
                                      + Type + ":"
                                      + Value;
    }
  }
}