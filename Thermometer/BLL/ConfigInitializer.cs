using System.IO;
using Newtonsoft.Json;

namespace Thermometer.BLL
{
  public class ConfigInitializer
  {
    public static Config GetConfig()
    {
      return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
    }
  }
}