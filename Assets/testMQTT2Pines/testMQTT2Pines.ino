/*
 * Ejemplo de protocolo MQTT para ESP32
 * Al recibir un mensaje "on", prende un LED (D2)
 * Al recibir un mensaje "off", apaga un LEC (D2)
 * Al tocar la terminal D4, publica mensaje "forward"
 * Al tocar la terminal D15, publica mensaje "backward"
 * Fuentes:
 * https://randomnerdtutorials.com/esp32-mqtt-publish-subscribe-arduino-ide/
 * https://www.instructables.com/id/IOT-Made-Simple-Playing-With-the-ESP32-on-Arduino-/
 * https://forum.arduino.cc/index.php?topic=319836.0
*/

#include<WiFi.h>
#include <PubSubClient.h>
//Para instalar la biblioteca PubSubClien.h: "Sketch -> Include Library -> Manage Libraries... -> Type PubSub in Search field -> Install."

#define TOUCH_FORWARD T0 // ESP32 Pin D4
#define TOUCH_BACKWARD T3 //ESP32 Pin D15
#define LED_PIN 2

const char* ssid = "WIFI_NAME";//Nombre de wifi
const char* password = "WIFI_PASSWORD";//Password
const char* mqtt_server = "IP_BROKER";//IP del servidor con mosquitto broker
//const char* mqtt_server = "http://broker.mqtt-dashboard.com/"
WiFiClient espClient;
PubSubClient client(espClient);

long lastMsg = 0;
char msg[50];
//int value = 0;
bool published = false;
//int touchValueForward = 100;
//int touchValueBackward = 100;

void setup()
{
    Serial.begin(115200);
    setup_wifi();
    client.setServer(mqtt_server, 1883);
    client.setCallback(callback);
    pinMode(LED_PIN, OUTPUT);
    digitalWrite (LED_PIN, LOW);
}

void loop()
{
    if(!client.connected())
    {
        reconnect();
    }
    client.loop();
    long now = millis();
    //touchValueForward = touchRead(TOUCH_FORWARD);
    //touchValueBackward = touchRead(TOUCH_BACKWARD);
    if(touchRead(TOUCH_FORWARD) < 50 && touchRead(TOUCH_BACKWARD) >= 50)
    {
        delay(300);
        if(touchRead(TOUCH_FORWARD) < 50 && touchRead(TOUCH_BACKWARD) >= 50)
        {
            if(!published)
            {
                Serial.println("Forward");
                //client.publish("topic","Mensaje");
                client.publish("esp32/input", "Forward");
                published = true;
            }   
        }
    }
    else if(touchRead(TOUCH_BACKWARD) < 50 && touchRead(TOUCH_FORWARD) >= 50)
    {
        delay(300);
        if(touchRead(TOUCH_BACKWARD) < 50 && touchRead(TOUCH_FORWARD) >= 50)
        {
            if(!published)
            {
                Serial.println("Backward");
                client.publish("esp32/input", "Backward");
                published = true;
            }   
        }
    }
    else
    {
        if(published)
        {
            Serial.println("Pin Free");
            client.publish("esp32/input","Pin Free");
            published = false;   
        }
    }
}

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
            digitalWrite(LED_PIN, HIGH);
        }
        else if(messageTemp=="off")
        {
            Serial.println("off");
            digitalWrite(LED_PIN, LOW);
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
