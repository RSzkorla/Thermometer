namespace Thermometer.Models
{
  public interface ISensory
  {
    double GetTemperature();
    string GetDeviceId();
  }
}