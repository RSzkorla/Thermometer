using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Thermometer.BLL
{
  public static class Engine
  {
    public static Config Config = ConfigInitializer.GetConfig();
    public static List<ISensory> Sensors;
    public static List<List<double>> RecentReadings;
    public static List<TimeStamp> CollectedTimeStamps;
    public static ViewAlerter ViewAlerter;
    public static GsmAlerter GsmAlerter;

    static Engine()
    {
      Sensors = EspSensorsInitializer.GetSensors();
      ViewAlerter = new ViewAlerter();
      GsmAlerter = new GsmAlerter();

      RecentReadings = new List<List<double>>
      {
        new List<double>
        {
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature(),
          Sensors.ElementAt(0).GetTemperature()
        },
        new List<double>
        {
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature(),
          Sensors.ElementAt(1).GetTemperature()
        }
      };
      CollectedTimeStamps = new List<TimeStamp> {new TimeStamp("STARTUP", "System started")};
    }


    public static bool CheckWarningRange(double temperature)
    {
      if (temperature < Config.LowerWarnBorder || temperature > Config.UpperWarnBorder) return true;
      return false;
    }

    public static bool CheckAlarmRange(double temperature)
    {
      if ((temperature < Config.LowerAlarmBorder || temperature > Config.UpperAlarmBorder)&&temperature > -100) return true;
      return false;
    }

    public static string GetRecentReadings()
    {
      return RecentReadings.GetDoubleValuesInOneStringFromTwoDimList();
    }

    public static string Monitoring()
    {
      double average = 0, sum = 0;
      for (var i = 0; i < Sensors.Count; i++)
      {
        var sensor = Sensors[i];
        var reading = sensor.GetTemperature();
        sum += reading;
        RecentReadings.ElementAt(i).PushToList(reading);
      }

      average = sum / Sensors.Count;



      if (CheckWarningRange(average) && !CheckAlarmRange(average))
      {
        ViewAlerter.SendWarning("Warning");
        CollectedTimeStamps.Add(new TimeStamp("WARNING", "Temperature in warning range"));
      }

      if (CheckAlarmRange(average))
      {
        ViewAlerter.SendAlert("ALERT Temperature is out of allowed range!");
        GsmAlerter.SendAlert("Alert!!! Temperature is out of allowed range!");
        CollectedTimeStamps.Add(new TimeStamp("ALERT", "Temperature out of allowed range!"));

      }

      return null;
    }

    public static string CollectData()
    {
      var strb = new StringBuilder();
      for (var i = 0; i < Sensors.Count; i++)
        strb.Append("S" + i + ":").Append(RecentReadings[i][0]).Append(" ");
      CollectedTimeStamps.Add(new TimeStamp("READ", strb.ToString()
      ));
      return null;
    }

    public static string GenerateReport()
    {
      var strb = new StringBuilder();
      strb.AppendLine("Report " + DateTime.Now);
      strb.AppendLine("Values " + Config.GetBorderValuesInOneString());
      strb.AppendLine();
      CollectedTimeStamps.ForEach(x => strb.AppendLine(x.ToString()));
      CollectedTimeStamps = new List<TimeStamp>();

      var fileName = (DateTime.Today + " " + Guid.NewGuid().ToString().Substring(0, 8) + ".txt").Replace(':', '-');
      var path = Path.Combine(Environment.CurrentDirectory, "Reports", fileName);
      File.WriteAllText(path, strb.ToString());
      return null;
    }

    public static void RunTechnicalPage()
    {
      const string technicalPageUrl = @"http://localhost:5001/Update/Index";
      ("explorer " + technicalPageUrl).Bash();
    }
  }
}