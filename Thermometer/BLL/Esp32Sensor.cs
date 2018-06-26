using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Thermometer.BLL
{
  public class Esp32Sensor : ISensory
  {

    private string _espIpAddress = "192.168.4.1";
    private string _description = "Remote Sensor";
    private int _sensorID = 0;

    public string GetDescription()
    {
      return _description;
    }

    public string GetDeviceId()
    {
      return _espIpAddress + "/" + _sensorID;
    }

    public double GetTemperature()
    {
      string path = "http://" + _espIpAddress + ":80/get";
      using (var client = new HttpClient())
      {
        using (var res = client.GetAsync(path).Result)
          using (var content = res.Content)
          {
            string data = content.ReadAsStringAsync().Result;
            return Convert.ToDouble(data);
          }
      }
    }
  }
}
