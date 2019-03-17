using System;
using System.Globalization;
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
    private bool sendAlert = true;


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
          if (!res.IsSuccessStatusCode && sendAlert)
          {
            sendAlert = false;
            Engine.GsmAlerter.SendAlert("Error with sensor "+ GetDeviceId()+". Intervention needed");
            return -999;
          }

          using (var content = res.Content)
          {
            readings++;
            string data = content.ReadAsStringAsync().Result.Split(',').ElementAt(_sensorID);
            var value = Convert.ToDouble(data, new NumberFormatInfo { NumberDecimalSeparator = "."});
            if (value == -999.00)
            {
              errorReadings++;

              if ((errorReadings / readings * 100) > 10 && sendAlert)
              {
                Engine.GsmAlerter.SendAlert("Error with sensor " + GetDeviceId() + ". Intervention needed");
                sendAlert = false;
              }

              return lastValue;
            }
            sendAlert = true;
            lastValue = value;
            if (readings != int.MaxValue - 1) return value;
            readings = 0;
            errorReadings = 0;
            return value;
          }
        }
      }
    }
  }
}