#include <WiFi.h>
#include <FS.h>
#include <AsyncTCP.h>
#include <ESPAsyncWebServer.h>
#include "DHT.h"

#define DHTPIN 23  
#define DHTTYPE DHT22 

DHT dht(DHTPIN, DHTTYPE);
float localHum = 0;
float localTemp = 0;
const char *ssid = "MyESP32AP";
const char *password = "testpassword";
String data = "";
AsyncWebServer server(80);

void setup()
{
  Serial.begin(115200);
  delay(1000); // give me time to bring up serial monitor
  dht.begin();
  WiFi.softAP(ssid, password);
  Serial.print("IP address: ");
  Serial.println(WiFi.softAPIP());
  server.on("/get", HTTP_GET, [](AsyncWebServerRequest *request){
    request->send(200, "text/plain", data);
  });
 
  server.begin();
}

void loop()
{
  float lastTemp=localTemp;
  localTemp=dht.readTemperature();
  if(isnan(localTemp)){
    localTemp=lastTemp;
  }
  data=localTemp;
  Serial.println(data);
  delay(500);
}


