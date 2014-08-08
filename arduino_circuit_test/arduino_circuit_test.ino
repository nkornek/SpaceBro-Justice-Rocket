int pinvolt = analogRead(0);

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  pinvolt = analogRead(2);
  Serial.println (pinvolt);
  
}
