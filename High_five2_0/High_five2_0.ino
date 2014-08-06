
int target1 = 1020;
int target2 = 1022;
int state[6] = {0, 0, 0, 0, 0, 0};

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  for(int pin=0; pin <3; pin++)
  {
    if (analogRead(pin) >= target1 & state[pin] == 0)
    {
      Serial.println (pin+1);
      state[pin] = 1;
    }
    else if (analogRead(pin) < target1 & state[pin] == 1)
    {
      state[pin] = 0;
    }
  }
    for(int pin=3; pin <6; pin++)
  {
    if (analogRead(pin) >= target2 & state[pin] == 0)
    {
      Serial.println (pin+1);
      state[pin] = 1;
    }
    else if (analogRead(pin) < target2 & state[pin] == 1)
    {
      state[pin] = 0;
    }
  }
  
}
