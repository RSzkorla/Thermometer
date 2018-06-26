using System;
using System.Linq;

namespace Thermometer.BLL
{
  public class ProxySensor:ISensory
  {
    private double _lowerRngBorder = 10.0;
    private double _upperRngBorder = 25.0;
    private readonly string _id= Guid.NewGuid().ToString();
    private static readonly Random _rng = new Random();
    public double GetTemperatureAsync() => Math.Round((_rng.NextDouble() * (_upperRngBorder - _lowerRngBorder) + _lowerRngBorder), 3);

    public string GetDeviceId() => _id.Take(5).ToString();

    public string GetDescription()
    {
      return "Proxy sensor " + _id.Take(5);
    }
  }
}