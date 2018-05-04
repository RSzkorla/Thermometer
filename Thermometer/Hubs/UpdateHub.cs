using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Thermometer.BLL;

namespace Thermometer.Hubs
{
    public class UpdateHub: Hub
    {
      public async Task Send(string message)
      {
        await Clients.All.SendAsync("Update", Engine.GetReadings());
      }
  }
}
