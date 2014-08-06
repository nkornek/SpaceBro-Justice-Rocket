
int target = 1020;
int state[6] = {0, 0, 0, 0, 0, 0};

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  for(int pin=0; pin < 6; pin++)
  {
    if (analogRead(pin) >= target & state[pin] == 0)
    {
      Serial.println (pin+1);
      state[pin] = 1;
    }
    else if (analogRead(pin) < target & state[pin] == 1)
    {
      state[pin] = 0;
    }
  }
  delay(10);
}
