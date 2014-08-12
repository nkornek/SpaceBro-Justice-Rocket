
int target[6] = {900, 1000, 1010, 1023, 1022, 1010};
int state[6] = {0, 0, 0, 0, 0, 0};
 
void setup() {
  
  Serial.begin(9600);
}

void loop() {
  for(int pin=0; pin <6; pin++)
  {
    if (analogRead(pin) >= target[pin] & state[pin] == 0)
    {
      Serial.println (pin+1);
      state[pin] = 1;
    }
    else if (analogRead(pin) < target[pin] & state[pin] == 1)
    {
      state[pin] = 0;
    }
  }
  
}
