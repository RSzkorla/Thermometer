using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.SignalR;
using Thermometer.BLL;

namespace Thermometer.Hubs
{
  public class UpdateHub : Hub
  {
    public override async Task OnConnectedAsync()
    {
      //HostingApplication.Context.User.Identity.Name
      await Clients.All.SendAsync("SendAction", "", "joined");
    }

    public override async Task OnDisconnectedAsync(Exception ex)
    {
      await Clients.All.SendAsync("SendAction", "", "left");
    }

    public async Task Send(string message)
    {
      await Clients.All.SendAsync("SendMessage", "", Engine.GetRecentReadings());
    }

    public async Task Update(string message)
    {
      await Clients.Caller.SendAsync("Update", "", Engine.Monitoring());
    }

    public async Task Collect(string message)
    {
      await Clients.Caller.SendAsync("Collect", "", Engine.CollectData());
    }

    public async Task Report(string message)
    {
      await Clients.Caller.SendAsync("Collect", "", Engine.GenerateReport());
    }
    public async Task SendAlert(string message)
    {
      await Clients.All.SendAsync("Alert", message);
    }

    public async Task SendWarning(string message)
    {
      await Clients.All.SendAsync("Warning", message);
    }
    public UpdateHub()
    {
      Engine.ViewAlerter.Hub = this;
    }
  }
}
