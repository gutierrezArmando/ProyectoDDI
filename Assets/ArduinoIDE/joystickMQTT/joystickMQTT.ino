#include<WiFi.h>
#include <PubSubClient.h>

#define ANALOG_EJE_X 34
#define ANALOG_EJE_Y 35

#define TERMINAL_DISPARO T5 //GPIO12
#define TERMINAL_RECARGA T4 //GPIO13

#define LED_OFFLINE 33
#define LED_ONLINE 32
const char* ssid = "w1f1@p3rryt05";//Nombre de wifi
const char* password = "z0w1eYn1n0";//Password
const char* mqtt_server = "192.168.1.77";//IP del servidor con mosquitto broker
//const char* mqtt_server = "http://broker.mqtt-dashboard.com/"
WiFiClient espClient;
PubSubClient client(espClient);

long lastMsg = 0;
char msg[50];
bool published_y = false;
bool published = false;
bool published_x = false;
bool published_shoot = false;
int valor_x;
int valor_y;

void setup() {
    // put your setup code here, to run once:
    Serial.begin(115200);
    pinMode(LED_OFFLINE, OUTPUT);
    pinMode(LED_ONLINE, OUTPUT);
    digitalWrite(LED_OFFLINE, HIGH);
    digitalWrite(LED_ONLINE, LOW);
    setup_wifi();
    client.setServer(mqtt_server, 1883);
    client.setCallback(callback);
}

void loop() {
    
    if(!client.connected()){
        reconnect();
    }
    //delay(2000);
    client.loop();
    long now = millis();
    //leerEjeY();
//    leerEje(1, &published_y);
    leerEje(2, &published_x);
    leerEje(1, &published_y);
    disparoRecarga();
}

void disparoRecarga() {
    if(touchRead(TERMINAL_DISPARO) < 50 && touchRead(TERMINAL_RECARGA) >= 50)
    {
        delay(10);
        if(touchRead(TERMINAL_DISPARO) < 50 && touchRead(TERMINAL_RECARGA) >= 50)
        {
            if(!published_shoot)
            {
                Serial.println("Shoot");
                //client.publish("topic","Mensaje");
                client.publish("esp32/terminals", "Shoot");
                published_shoot = true;
            }   
        }
    }
    else if(touchRead(TERMINAL_RECARGA) < 50 && touchRead(TERMINAL_DISPARO) >= 50)
    {
        delay(10);
        if(touchRead(TERMINAL_RECARGA) < 50 && touchRead(TERMINAL_DISPARO) >= 50)
        {
            if(!published_shoot)
            {
                Serial.println("Reload");
                client.publish("esp32/terminals", "Reload");
                published_shoot = true;
            }   
        }
    }
    else
    {
        if(published_shoot)
        {
            Serial.println("NonShootNonReload");
            client.publish("esp32/terminals","NonShootNonReload");
            published_shoot = false;
        }
    }
}
//1 -> EjeY
//2 -> EjeX
void leerEje(int eje, bool *published) {
    int valor=2000;
    char cadenaValor[20];
    valor = analogRead(eje==1?ANALOG_EJE_Y:ANALOG_EJE_X);
    Serial.print(eje==1?"Valor en Y: ":"Valor en X: ");
    Serial.println(valor);
    if(valor > 3000)
    {
        if(!(*published))
        {
            Serial.println(eje==1?"Forward":"Left");
            //client.publish("esp32/input", eje==1?"Forward":"Left");
            client.publish(eje==1?"esp32/input":"esp32/rotate", eje==1?"Forward":"Left");
            *published = true;
        }
    }
    else if(valor < 1000)
    {
        if(!(*published))
        {
            Serial.println(eje==1?"Backward":"Right");
            //client.publish("esp32/input", eje==1?"Backward":"Right");
            client.publish(eje==1?"esp32/input":"esp32/rotate", eje==1?"Backward":"Right");
            *published = true;
        }
    }
    else
    {
        if((*published))
        {
            Serial.println(eje==1?"Pin Y Free":"Pin X Free");
            //client.publish("esp32/input",eje==1?"Pin Y Free":"Pin X Free");
            client.publish(eje==1?"esp32/input":"esp32/rotate",eje==1?"Pin Y Free":"Pin X Free");
            *published = false;   
        }
    }
}/*Fin leerEje*/

void setup_wifi()
{
    delay(10);
    Serial.println();
    Serial.print("Conectando a ");
    Serial.println(ssid);

    WiFi.begin(ssid, password);

    while(WiFi.status() != WL_CONNECTED)
    {
        delay(500);
        Serial.print(".");
    }
    digitalWrite(LED_ONLINE, HIGH);
    digitalWrite(LED_OFFLINE, LOW);
    Serial.println("");
    Serial.println("Wifi Conectada");
    Serial.print("IP Address: ");
    Serial.println(WiFi.localIP());
}/*Fin setup_wifi*/

void callback(char* topic, byte* message, unsigned int length)
{
    Serial.print("Mensaje recibido en topic: ");
    Serial.print(topic);
    Serial.print(". Mensaje: ");
    String messageTemp;
    for (int i = 0; i < length; i++)
    {
        Serial.print((char)message[i]);
        messageTemp += (char)message[i];
    }
    Serial.println();
    if(String(topic) == "esp32/output")
    {
        Serial.print("Cambiando pin de salida a ");
        if(messageTemp=="on")
        {
            Serial.println("on");
            //digitalWrite(LED_PIN, HIGH);
        }
        else if(messageTemp=="off")
        {
            Serial.println("off");
            //digitalWrite(LED_PIN, LOW);
        }
    }
}/*Fin funcion callback*/

void reconnect()
{
    // Loop until we're reconnected
    while(!client.connected())
    {
        Serial.print("Intentando conexion MQTT...");
        if(client.connect("ESP32Client"))
        {
            Serial.println("Conectado");
            //Subscribe
            client.subscribe("esp32/output");
        }
        else
        {
            Serial.print("Fallido, rc=");
            Serial.print(client.state());
            Serial.println(" Intento nuevamente en 5 segundos");
            // Wait 5 seconds before retrying
            delay(5000);
        }
    }
}/*Fin reconnect*/
