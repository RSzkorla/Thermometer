using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thermometer.BLL
{
  public class Monitor
  {

    private void DoStuff()
    {
      while (true)
      {
        Engine.Monitoring();
        Task.Delay(100);
      }
      
    }

    public void Start()
    {
      DoStuff();
    }
  }
}