#include <Wire.h>
#include <LiquidCrystal_I2C.h>

LiquidCrystal_I2C lcd(0x27,16,2); // set the LCD address to 0x3F for a 16 chars and 2 line display

void setup()
{
  Serial.begin(115200);
  Serial.println("Serial Monitor on 115200");
  Serial.println("ESP32 I2C Liquid Crystal Display");
  
  lcd.init(); // initialize the lcd
  lcd.backlight();
}

void loop()
{
  Serial.println("Loop");
  
  // Print a message to the LCD.
  lcd.setCursor(0,0);
  lcd.print("Hello world");
  //lcd.setCursor(1,0);
  //lcd.print("ESP32 I2C LCD");
  
  delay(10000);
}
