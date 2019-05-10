#!/bin/bash
dotnet /home/pi/Thermomether/linux-arm/publish/Thermometer.dll &
sleep 60 &&
guid=$(<guid)
x-www-browser http://localhost:5001/Update/Index?guid=$guid
