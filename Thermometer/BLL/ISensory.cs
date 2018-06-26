namespace Thermometer.BLL
{
  public interface ISensory
  {
    double GetTemperature();
    string GetDeviceId();
    string GetDescription();
  }
}