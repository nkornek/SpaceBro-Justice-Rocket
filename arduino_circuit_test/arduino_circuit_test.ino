int pinvolt = analogRead(1);

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  pinvolt = analogRead(3);
  Serial.println (pinvolt);
  
}
