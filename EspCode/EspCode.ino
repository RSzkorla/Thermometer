#include <dht.h>

#include <WiFi.h>
#include <FS.h>
#include <AsyncTCP.h>
#include <ESPAsyncWebServer.h>


#define DHTPIN 23  
#define DHTTYPE DHT22 

#define DHT22_PIN 23

//DHT dht(DHTPIN, DHTTYPE);
float localHum = 0;
float localTemp = 0;
const char *ssid = "MyESP32AP";
const char *password = "testpassword";
String data = "";
AsyncWebServer server(80);
dht DHT;

void setup()
{
  Serial.begin(115200);
  delay(1000); // give me time to bring up serial monitor
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
  data=localTemp;
  Serial.println(data);
  delay(500);
}


