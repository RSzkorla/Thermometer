using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Hosting;

namespace Thermometer.BLL
{
  public class GsmAlerter : IAlerter
  {
    private readonly IHostingEnvironment _env;
    public bool CanISendAlert = true;
    private SerialPortCommunication serialPort;
    public List<string> PhoneNumberList { get; set; }

    public GsmAlerter()
    {
      _env = Startup.Environment;
      PhoneNumberList = Engine.Config.PhoneNumbers;
      serialPort = new SerialPortCommunication();
    }

    

    public void SendWarning(string message)
    {
    }

    public void SendAlert(string message)
    {
      //var r = Environment.NewLine;
      //if (CanISendAlert)
      //{
      //  var strb = new StringBuilder();
      //  strb.Append("#!/usr/bin/expect -f" + r +
      //              "spawn minicom -D /dev/ttyS0\\r\"" + r +
      //              "send \"AT\\r\"" + r +
      //              "expect" + r);
      //  foreach (var number in PhoneNumberList)
      //    strb.Append("send \"AT + CMGF = 1\\r\"" + r +
      //                "expect" + r +
      //                $"send \"AT+CMGS=\\\"{number}\\\"\\r\"" + r +
      //                "expect" + r +
      //                $"send \"{message}\r\"" + r +
      //                "expect" + r +
      //                "send \"\032\"" + r);
      //  File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "script.sh"), strb.ToString());
      if (message == "temp")
      {
        foreach (var item in PhoneNumberList)
        {
          serialPort.SendSMS(item,"ALERT Temperature out of allowed range");
        }
        //var path = Path.Combine(Directory.GetCurrentDirectory(),"Scripts", "temp.sh");
        //path.Bash();
      }
      if (message == "device")
      {
        foreach (var item in PhoneNumberList)
        {
          serialPort.SendSMS(item, "ALERT Device problem. Probably power supply problem");
        }
        //var path = Path.Combine(Directory.GetCurrentDirectory(), "Scripts", "device.sh");
        //path.Bash();
      }


    
    }
  }
}