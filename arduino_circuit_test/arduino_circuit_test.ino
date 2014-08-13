int pinvolt = analogRead(1);

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  pinvolt = analogRead(1);
  Serial.println (pinvolt);
  
}
