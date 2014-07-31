
int threshhold = 1023;
int state[6] = {0, 0, 0, 0, 0, 0};
int willDisplay[6] = {0, 0, 0, 0, 0, 0};

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  for(int pin=0; pin < 6; pin++)
  {
    if (analogRead(pin) >= threshhold & state[pin] == 0)
    {
      willDisplay[pin] += 1;
      if (willDisplay[pin] >= 5)
      {
        Serial.println (pin+1);
        state[pin] = 1;
      }
    }
    else if (analogRead(pin) < threshhold & state[pin] == 1)
    {
      state[pin] = 0;
      willDisplay[pin] = 0;
    }
  }
  delay(5);
}
