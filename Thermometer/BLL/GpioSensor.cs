using System;
using System.IO;

namespace Thermometer.BLL
{
  public class GpioSensor:ISensory
  {
    private string _devicePath = "/sys/bus/w1/devices/";
    private string _deviceId = "28-000008b8db23";
    private string _description = "Gpio Sensor";
    public double GetTemperatureAsync()
    {
      try
      {
        StreamReader str = new StreamReader(_devicePath + _deviceId + "/w1_slave");
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

    public string GetDeviceId() => _deviceId;

    public string GetDescription() => _description;
  }
}