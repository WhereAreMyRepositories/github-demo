#define X_MIN_PIN           3
#ifndef X_MAX_PIN
  #define X_MAX_PIN         2
#endif
#define Y_MIN_PIN          14
#define Y_MAX_PIN          15
#define Z_MIN_PIN          18
#define Z_MAX_PIN          19


#define X_STEP_PIN         54
#define X_DIR_PIN          55
#define X_ENABLE_PIN       38
#define X_CS_PIN           53

#define Y_STEP_PIN         60
#define Y_DIR_PIN          61
#define Y_ENABLE_PIN       56
#define Y_CS_PIN           49

#define Z_STEP_PIN         46
#define Z_DIR_PIN          48
#define Z_ENABLE_PIN       62
#define Z_CS_PIN           40

int x;
int y;
int z;
int krokx = 0;
int kroky = 0;
int krokz = 0;
int tmp;
String s;
void setup() {
  // put your setup code here, to run once:
  Serial.begin(57600);
  pinMode(X_MAX_PIN, INPUT_PULLUP);
  pinMode(Y_MAX_PIN, INPUT_PULLUP);
  pinMode(Z_MAX_PIN, INPUT_PULLUP);

  pinMode(X_ENABLE_PIN, OUTPUT);
  digitalWrite(X_ENABLE_PIN, LOW);
  pinMode(X_DIR_PIN, OUTPUT);
  digitalWrite(X_DIR_PIN, LOW);
  pinMode(X_STEP_PIN,OUTPUT);

  pinMode(Y_ENABLE_PIN, OUTPUT);
  digitalWrite(Y_ENABLE_PIN, LOW);
  pinMode(Y_DIR_PIN, OUTPUT);
  digitalWrite(Y_DIR_PIN, LOW);
  pinMode(Y_STEP_PIN,OUTPUT);

  pinMode(Z_ENABLE_PIN, OUTPUT);
  digitalWrite(Z_ENABLE_PIN, LOW);
  pinMode(Z_DIR_PIN, OUTPUT);
  digitalWrite(Z_DIR_PIN, LOW);
  pinMode(Z_STEP_PIN,OUTPUT);
}

void loop() {
 x =  digitalRead(X_MAX_PIN);
 y =  digitalRead(Y_MAX_PIN);
 z =  digitalRead(Z_MAX_PIN);
//  Serial.print(x);
//  Serial.print(y);
//  Serial.println(z);

while( Serial.available()>0)
{
 s = Serial.readStringUntil('\n');
 
 sscanf(s.c_str(),"x%d y%d z%d",&krokx,&kroky,&krokz);
 
 }
    if(x == 0 && krokx > 0)
      { 
      digitalWrite(X_STEP_PIN, HIGH);
      krokx --;
      }
    if(y == 0 && kroky > 0)
      {
        digitalWrite(Y_STEP_PIN, HIGH);
        kroky--;
      }
    if(z == 0 && krokz > 0)
      {
        digitalWrite(Z_STEP_PIN, HIGH);
        krokz--;
      }
    delayMicroseconds(300);
    digitalWrite(X_STEP_PIN, LOW);
    digitalWrite(Y_STEP_PIN, LOW);
    digitalWrite(Z_STEP_PIN, LOW);
    delayMicroseconds(300);
  }
