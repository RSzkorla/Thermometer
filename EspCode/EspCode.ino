#include <dht.h>

#include <WiFi.h>
#include <FS.h>
#include <AsyncTCP.h>
#include <ESPAsyncWebServer.h>


#define DHTPIN 23  

#define DHTTYPE DHT22 
#define DHT22_PIN 23
#define DHT22_PIN1 21


//DHT dht(DHTPIN, DHTTYPE);
float localHum = 0;
float localTemp = 0;
float localTemp1 = 0;
const char *ssid = "ESPsensor0";
const char *password = "password";
String data = "";
AsyncWebServer server(80);
dht DHT;

void setup()
{
  Serial.begin(115200);
  delay(1000); 
  Serial.println("Starting, IP: 192.168.4.1");
  //dht.begin();
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
  uint32_t start = micros();
  int chk = DHT.read22(DHT22_PIN);
  uint32_t stop = micros();
  float lastTemp=localTemp;
  localTemp=DHT.temperature;
  if(isnan(localTemp)){
    localTemp=lastTemp;
    Serial.print("err ");
  }
    uint32_t start1 = micros();
    int chk1 = DHT.read22(DHT22_PIN1);
    uint32_t stop1 = micros();
  float lastTemp1=localTemp1;
  localTemp1=DHT.temperature;
  if(isnan(localTemp1)){
    localTemp=lastTemp1;
    Serial.print("err ");
  }
  data=String(localTemp)+","+String(localTemp1);
  Serial.println(data);
  delay(2000);
}


