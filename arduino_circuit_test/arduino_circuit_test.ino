int pinvolt = analogRead(1);

void setup() {
  
  Serial.begin(9600);
}

void loop() {
  pinvolt = analogRead(5);
  Serial.println (pinvolt);
  
}
