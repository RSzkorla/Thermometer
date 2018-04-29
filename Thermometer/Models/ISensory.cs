namespace Thermometer.Models
{
  internal interface ISensory
  {
    double GetTemperature();
    string GetDeviceId();
  }
}