using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thermometer.Models
{
    public interface IAlerter
    {
      void SendAlert(string message);
    }
}
