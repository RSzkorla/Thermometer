using System;
using System.IO;

namespace Thermometer.Models
{
  public class GpioSensor:ISensory
  {
    private string _devicePath = "/sys/bus/w1/devices/28-000008b8db23";
    public double GetTemperature()
    {
      try
      {
        StreamReader str = new StreamReader(_devicePath + "/w1_slave");
        string content = string.Empty;
        while (!str.EndOfStream)
        {
          content = str.ReadLine();
        }
        string tempString = content.Substring(content.Length - 6, 6).Replace("=", "");
        int tempInt = Convert.ToInt32(tempString);
        double temp = tempInt / 1000.0;
        return temp;
      }
      catch (Exception)
      {
        return double.MinValue;
      }
    }

    public string GetDeviceId()
    {
      throw new System.NotImplementedException();
    }
  }
}