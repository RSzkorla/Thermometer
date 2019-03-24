using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Thermometer.BLL
{
  public class ConfigInitializer
  {
    public static Config GetConfig() => JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
  }
  
}
