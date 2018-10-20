using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using Vuforia;

public class click_btnLigar : MonoBehaviour, IVirtualButtonEventHandler {

    public GameObject Objetbtn;
    public string topico = "";
    private MqttClient client;
    private bool troca = true;
    // Use this for initialization
    void Awake()
    {
        client = new MqttClient(IPAddress.Parse("192.168.0.100"), 1883, false, null);
        client.Connect(Guid.NewGuid().ToString(), "pi", "raspberry");
    }

    void Start()
    {
        Objetbtn = GameObject.Find("btnLigar");
        Objetbtn.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
    }

    public void OnButtonPressed(VirtualButtonAbstractBehaviour vb)
    {
        if(troca)
            client.Publish(topico, System.Text.Encoding.UTF8.GetBytes("1"), MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, true);
        troca = false;
    }

    public void OnButtonReleased(VirtualButtonAbstractBehaviour vb)
    {
        troca = true;
    }


	
	// Update is called once per frame
	void Update () {
		
	}
}
