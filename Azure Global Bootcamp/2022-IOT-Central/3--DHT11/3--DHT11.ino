/*
 * LED Blinking
 * Serial Monitoring
 * DHT11
 */

#include "DHT.h"
 
#define LED 5
#define DHTPIN 12
#define DHTTYPE DHT11

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  // Connect to Serial Monitor
  Serial.begin(115200);
  
  // Configure PIN 5 as Output
  pinMode (LED, OUTPUT);
  dht.begin();

  Serial.println("ESP32: Started successfully.");
}
void loop() {
  // Turn ON LED
  digitalWrite (LED, HIGH);
  delay(1000);

  // Read DHT
  float h = dht.readHumidity();
  float t = dht.readTemperature(); //Celsius

  // Check if any reads failed and exit early (to try again).
  if (isnan(h) || isnan(t)) {
    Serial.println(F("Failed to read from DHT sensor!"));
  } else {
    // Compute heat index in Celsius (isFahreheit = false)
    float hic = dht.computeHeatIndex(t, h, false);
    
    Serial.print(F("Humidity: "));
    Serial.print(h);
    Serial.print(F("% | Temperature: "));
    Serial.print(t);
    Serial.print(F("°C | Heat index: "));
    Serial.print(hic);
    Serial.print(F("°C "));
  }
  
  // Turn OFF LED
  digitalWrite (LED, LOW);
  delay(1000);
  Serial.println();
}
