/*
 * LED Blinking
 * Serial Monitoring
 * DHT11
 * Soil Moisture
 */

#include "DHT.h"
 
#define LED 12

#define DHTPIN 14
#define DHTTYPE DHT11

#define SOIL A0

DHT dht(DHTPIN, DHTTYPE);

void setup() {
  // Connect to Serial Monitor
  Serial.begin(115200);
  
  // Configure PIN 5 as Output
  pinMode(LED, OUTPUT);
  
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

  // Read Soil Moisture
  int soil_moisture = analogRead(SOIL);
  //int soil_moisture_mapped = map(soil_moisture, 550, 0, 0, 100);
  int soil_moisture_mapped = ( 100.00 - ( (analogRead(soil_moisture) / 1023.00) * 100.00 ) );
  Serial.println();
  Serial.print("Soil Moisture: ");
  Serial.print(soil_moisture);
  Serial.print(" | Percentage: ");
  Serial.print(soil_moisture_mapped);
  Serial.println("%");
  
  // Turn OFF LED
  digitalWrite (LED, LOW);
  delay(1000);
  Serial.println();
}
