using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thermometer.BLL
{
    public interface IAlerter
    {
      void SendWarning(string message);
      void SendAlert(string message);
    }
}
