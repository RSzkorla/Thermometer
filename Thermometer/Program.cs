using System;
using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Thermometer
{
  public class Program
  {
    public static void Main(string[] args)
    {
      File.WriteAllText("guid",Guid.NewGuid().ToString());
      Console.WriteLine(File.ReadAllText("guid"));
      Console.WriteLine(Environment.CurrentDirectory);
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://*:5000;http://localhost:5001")
        .UseStartup<Startup>()
        .Build();
    }
  }
}