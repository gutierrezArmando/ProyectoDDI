#include<WiFi.h>
#include <PubSubClient.h>

#define ANALOG_EJE_X 34
#define ANALOG_EJE_Y 35
const char* ssid = "w1f1@p3rryt05";//Nombre de wifi
const char* password = "z0w1eYn1n0";//Password
const char* mqtt_server = "192.168.1.77";//IP del servidor con mosquitto broker
//const char* mqtt_server = "http://broker.mqtt-dashboard.com/"
WiFiClient espClient;
PubSubClient client(espClient);

long lastMsg = 0;
char msg[50];
bool published = false;

int valor_x;
int valor_y;

void setup() {
    // put your setup code here, to run once:
    Serial.begin(115200);
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
    valor_y = analogRead(ANALOG_EJE_Y);
    Serial.println(valor_y);
    //touchValueForward = touchRead(TOUCH_FORWARD);
    //touchValueBackward = touchRead(TOUCH_BACKWARD);
    if(valor_y > 3000)
    {
        if(!published)
        {
            Serial.println("Forward");
            //client.publish("topic","Mensaje");
            client.publish("esp32/input", "Forward");
            published = true;
        }
    }
    else if(valor_y < 1000)
    {
        if(!published)
        {
            Serial.println("Backward");
            client.publish("esp32/input", "Backward");
            published = true;
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
