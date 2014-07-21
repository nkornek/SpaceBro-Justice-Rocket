
int threshhold = 1020;

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  for(int pin=0; pin < 6; pin++)
  {
    if (analogRead(pin) > threshhold)
    {
      Serial.println (pin+1);
    }
  }
  delay(100);
}
