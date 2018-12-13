using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class M2MQTTMain : M2MqttUnityClient {

    public string msg;
    public String MQTTmessage;

    protected override void Start()
    {
        SetBrokerAddress("192.168.1.77");
        SetBrokerPort("1883");
        Debug.Log("Started application");
        this.autoConnect = true;
        base.Start();
    }

    public void SetBrokerAddress(string brokerAddress)
    {
        this.brokerAddress = brokerAddress;
    }

    public void SetBrokerPort(string brokerPort)
    {
        int.TryParse(brokerPort, out this.brokerPort);
    }

    protected override void OnConnecting()
    {
        base.OnConnecting();
        Debug.Log("Connecting to broker " + brokerAddress + ":" + brokerPort + "...\n");
    }

    protected override void OnConnected()
    {
        base.OnConnected();
        Debug.Log("Connected to " + brokerAddress + ":" + brokerPort);
    }

    protected override void SubscribeTopics()
    {
        client.Subscribe(new string[] { "esp32/input" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { "esp32/input" });
    }

    protected override void OnConnectionFailed(string errorMessage)
    {
        Debug.Log("Connection Failed!");
    }

    protected override void OnConnectionLost()
    {
        Debug.Log("Connection Lost!");
    }

    protected override void DecodeMessage(string topic, byte[] message)
    {
        msg = System.Text.Encoding.UTF8.GetString(message);
        Debug.Log("Received: " + msg);
        //base.DecodeMessage(topic, message);
    }

    protected override void Update()
    {
        base.Update();/*
        if (msg.Equals("Forward"))
        {
            MQTTmessage = "Forward";
        }
        else if (msg.Equals("Backward"))
        {
            MQTTmessage = "Backward";
        }
        else
            MQTTmessage = msg;

        SendMovement();
        */
    }

    private void OnDestroy()
    {
        Disconnect();
    }
    /*
    public void PublishLedOn(){
        client.Publish("esp32/output", System.Text.Encoding.UTF8.GetBytes("on"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Led On");
    }

    public void PublisLedOff(){
        client.Publish("esp32/output", System.Text.Encoding.UTF8.GetBytes("off"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, false);
        Debug.Log("Led off");
    }*/
}
