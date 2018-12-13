int ladoy = A0; // Entrada de referencia para el eje de las Y's.
int ladox = A1; // Entrada de referencia para el eje de las X's.
int valor;
int valor2;

void setup() {
  // put your setup code here, to run once:
  Serial.begin(115200);
}

void loop() {
    // put your main code here, to run repeatedly:
    leerEjeX();
    leerEjeY();
    
}

void leerEjeX() {
    valor = analogRead(ladox); // Durante todo el proceso interrogaremos
    if (valor > 700) // el valor en el que se encuentra posicionado
    {
        Serial.println("Right");
    }
    else if (valor < 400)
    {
        Serial.println("Left");
    }
    else
    {
        Serial.println("Normal");
    }
}

void leerEjeY() {
    valor2 = analogRead(ladoy);
    if (valor2 > 700)
    {
        Serial.println("UP");
    }
    else if (valor2 < 400)
    {
        Serial.println("Down");
    }
    else
    {
        Serial.println("Normal");
    }
}
