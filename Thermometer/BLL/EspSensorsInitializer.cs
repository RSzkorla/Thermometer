using System.Collections.Generic;

namespace Thermometer.BLL
{
  public class EspSensorsInitializer
  {
    public static List<ISensory> GetSensors()
    {
      var l = new List<ISensory>
      {
        new Esp32Sensor {_espIpAddress = "192.168.4.1", _sensorID = 0},
        new Esp32Sensor {_espIpAddress = "192.168.4.1", _sensorID = 1}
      };
      return l;
    }
  }
}