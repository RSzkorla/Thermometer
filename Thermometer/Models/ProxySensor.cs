using System;

namespace Thermometer.Models
{
  public class ProxySensor:ISensory
  {
    private double _lowerRngBorder = 10.0;
    private double _upperRngBorder = 25.0;
    private readonly string _id= Guid.NewGuid().ToString();
    private static readonly Random _rng = new Random();
    public double GetTemperature() => Math.Round((_rng.NextDouble() * (_upperRngBorder - _lowerRngBorder) + _lowerRngBorder), 3);

    public string GetDeviceId() => _id;
  }
}