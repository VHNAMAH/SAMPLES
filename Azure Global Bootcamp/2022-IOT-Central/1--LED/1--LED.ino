/*
 * LED Blinking
 */
const int LED = 5;

void setup() {
  // Configure PIN 5 as Output
  pinMode (LED, OUTPUT);
}
void loop() {
  // Turn ON LED
  digitalWrite (LED, HIGH);
  delay(1000);
  
  // Turn OFF LED
  digitalWrite (LED, LOW);
  delay(1000);
}
