using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using M2MqttUnity;

public class M2MQTTRotate : M2MQTTMain {

    protected override void SubscribeTopics()
    {
        client.Subscribe(new string[] { "esp32/rotate" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
    }

    protected override void UnsubscribeTopics()
    {
        client.Unsubscribe(new string[] { "esp32/rotate" });
    }


    protected override void Update()
    {
        base.Update();
        Debug.Log(msg);
    }

    private void OnDestroy()
    {
        Disconnect();
    }
}
