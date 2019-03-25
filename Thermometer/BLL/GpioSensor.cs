using System;
using System.IO;

namespace Thermometer.BLL
{
  public class GpioSensor : ISensory
  {
    private readonly string _description = "Gpio Sensor";
    private readonly string _deviceId = "28-000008b8db23";
    private readonly string _devicePath = "/sys/bus/w1/devices/";

    public double GetTemperature()
    {
      try
      {
        var str = new StreamReader(_devicePath + _deviceId + "/w1_slave");
        var content = string.Empty;
        while (!str.EndOfStream) content = str.ReadLine();
        var tempString = content.Substring(content.Length - 6, 6).Replace("=", "");
        var tempInt = Convert.ToInt32(tempString);
        var temp = tempInt / 1000.0;
        return temp;
      }
      catch (Exception)
      {
        return double.MinValue;
      }
    }

    public string GetDeviceId()
    {
      return _deviceId;
    }

    public string GetDescription()
    {
      return _description;
    }
  }
}