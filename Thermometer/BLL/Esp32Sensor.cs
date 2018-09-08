using System;
using System.Linq;
using System.Net.Http;

namespace Thermometer.BLL
{
  public class Esp32Sensor : ISensory
  {
    public string _espIpAddress = "192.168.4.1";
    public string _description = "Remote Sensor";
    public int _sensorID = 0;
    private double lastValue = 0;
    private long readings=0;
    private long errorReadings = 0;


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
      string uri = "http://" + _espIpAddress + ":80/get";
      using (var client = new HttpClient())
      {
        using (var res = client.GetAsync(uri).Result)
        {
          if (!res.IsSuccessStatusCode)
          {
            Engine.GsmAlerter.SendAlert("Error with sensor "+ GetDeviceId()+". Intervention needed");
            return -999;
          }

          using (var content = res.Content)
          {
            readings++;
            string data = content.ReadAsStringAsync().Result.Split(',').ElementAt(_sensorID);
            var value = Convert.ToDouble(data);
            if (value == -999.00)
            {
              errorReadings++;
              if ((errorReadings/readings*100)>10) Engine.GsmAlerter.SendAlert("Error with sensor " + GetDeviceId() + ". Intervention needed");
              return lastValue;
            }
            lastValue = value;
            return value;
          }
        }
      }
    }
  }
}