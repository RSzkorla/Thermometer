using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Thermometer.BLL
{
  public class MonitorService : BackgroundService
  {
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
      while (!stoppingToken.IsCancellationRequested)
      {
        Engine.Monitoring();
        await Task.Delay(500, stoppingToken);
      }
    }
  }
}