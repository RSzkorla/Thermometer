using System;
using System.Linq;

namespace Thermometer.BLL
{
  public class ProxySensor:ISensory
  {
    private double _lowerRngBorder = 10.0;
    private double _upperRngBorder = 25.0;
    private int _functionParam = 0;
    private int _functionInrement = 1;
    private readonly string _id= Guid.NewGuid().ToString();
    private static readonly Random _rng = new Random();
    public double GetTemperature()
    {
      _functionParam += _functionInrement;
      if (_functionParam == 0) _functionInrement *= -1;
      if (_functionParam == 10) _functionInrement *= -1;
      return Math.Round((_functionParam*Math.Sin(_functionParam)+_lowerRngBorder), 3);
    }

    public string GetDeviceId() => _id.Take(5).ToString();

    public string GetDescription()
    {
      return "Proxy sensor " + _id.Take(5);
    }
  }
}