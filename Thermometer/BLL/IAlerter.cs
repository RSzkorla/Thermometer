namespace Thermometer.BLL
{
  public interface IAlerter
  {
    void SendWarning(string message);
    void SendAlert(string message);
  }
}