﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace Thermometer
{
  public class Program
  {
    public static void Main(string[] args)
    {
      BuildWebHost(args).Run();
    }

    public static IWebHost BuildWebHost(string[] args)
    {
      return WebHost.CreateDefaultBuilder(args)
        .UseUrls("http://*:80;http://localhost:80")
        .UseStartup<Startup>()
        .Build();
    }
  }
}