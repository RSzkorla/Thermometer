using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using RJCP.IO.Ports;

namespace Thermometer.BLL
{
  public class SerialPortCommunication
  {
    public void SendSMS(string phoneNr, string message)
    {
      SerialPortStream myPort = null;

      var ports = GetPortNames();
      foreach (var port in ports)
        if (port == "/dev/ttyS0")
        {
          myPort = new SerialPortStream("/dev/ttyS0", 115200, 8, Parity.None, StopBits.One);
          myPort.Open();
          if (!myPort.IsOpen)
          {
            Console.WriteLine("Error opening serial port");
            return;
          }

          Console.WriteLine("Port open");
        }

      if (myPort == null)
      {
        Console.WriteLine("No serial port /dev/ttyS0");
        return;
      }

      myPort.Handshake = Handshake.None;
      myPort.ReadTimeout = 10000;
      myPort.NewLine = "\r\n";


      Thread.Sleep(1000);

      myPort.Write("AT+CMGF=1\r");

      Thread.Sleep(1000);

      myPort.Write("AT+CMGS=\"" + phoneNr + "\"\r\n");

      Thread.Sleep(1000);

      myPort.Write(message + "\x1A");

      Thread.Sleep(1000);
      myPort.Close();
    }

    private string[] GetPortNames()
    {
      var p = (int) Environment.OSVersion.Platform;
      var serial_ports = new List<string>();

      // Are we on Unix?
      if (p == 4 || p == 128 || p == 6)
      {
        var ttys = Directory.GetFiles("/dev/", "ttyS0");
        foreach (var dev in ttys)
          if (dev.StartsWith("/dev/ttyS") || dev.StartsWith("/dev/ttyUSB") || dev.StartsWith("/dev/ttyACM") ||
              dev.StartsWith("/dev/ttyAMA"))
          {
            serial_ports.Add(dev);
            Console.WriteLine("Serial list: {0}", dev);
          }
      }

      return serial_ports.ToArray();
    }
  }
}