using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

	public int playerSpeed = 5;
	public float deltaRotate = 90f;
    public GameObject efectoPasos;
    public GameObject MQTTProtocol;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        Walk();
        Rotate();

		//Debug.Log("Updated...");
	}

    void Walk()
    {
        if (Input.GetKey(KeyCode.UpArrow) || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage == "Forward")
        {
            transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
            efectoPasos.SetActive(true);
        }
        if (Input.GetKey(KeyCode.DownArrow) || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage == "Backward")
        {
            transform.position = transform.position + Camera.main.transform.forward * -playerSpeed * Time.deltaTime;
            efectoPasos.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage == "Pin Y Free" || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage == "Pin X Free")
            efectoPasos.SetActive(false);
    }

    void Rotate()
    {
        if (Input.GetKey(KeyCode.RightArrow)||MQTTProtocol.GetComponent<M2MQTTRotate>().msg=="Right")
            transform.Rotate(new Vector3(0f, deltaRotate, 0f) * Time.deltaTime);
        if (Input.GetKey(KeyCode.LeftArrow) || MQTTProtocol.GetComponent<M2MQTTRotate>().msg == "Left")
            transform.Rotate(new Vector3(0f, -deltaRotate, 0f) * Time.deltaTime);
    }
	
}