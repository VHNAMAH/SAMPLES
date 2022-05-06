/*
 * LED Blinking
 */
const int LED = 5;

void setup() {
  // Connect to Serial Monitor
  Serial.begin(115200);
  
  // Configure PIN 5 as Output
  pinMode (LED, OUTPUT);

  Serial.println("ESP32: Started successfully.");
}
void loop() {
  // Turn ON LED
  digitalWrite (LED, HIGH);
  Serial.println("ESP32 :: LED : ON");
  delay(1000);
  
  // Turn OFF LED
  digitalWrite (LED, LOW);
  Serial.println("ESP32 :: LED : OFF");
  delay(1000);
}
