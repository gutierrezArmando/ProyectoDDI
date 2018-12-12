using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

	public int playerSpeed = 8;
	public float deltaRotate = 90f;
    public GameObject efectoPasos;
    public GameObject MQTTProtocol;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.UpArrow) || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage=="Forward")
        {
            transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
            efectoPasos.SetActive(true);
        }
        if (Input.GetKey(KeyCode.DownArrow) || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage == "Backward")
        {
            transform.position = transform.position + Camera.main.transform.forward * -playerSpeed * Time.deltaTime;
            efectoPasos.SetActive(true);
        }		
        if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(new Vector3(0f, deltaRotate, 0f) * Time.deltaTime);
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(new Vector3(0f, -deltaRotate, 0f) * Time.deltaTime);
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || MQTTProtocol.GetComponent<M2MQTTPrueba>().MQTTmessage=="Pin Free")
            efectoPasos.SetActive(false);
		//Debug.Log("Updated...");
	}
	
}