using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

	public int playerSpeed = 6;
	public float deltaRotate = 90f;

	// Use this for initialization
	void Start () {	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.UpArrow))
			transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        if(Input.GetKey(KeyCode.DownArrow))
	        transform.position = transform.position + Camera.main.transform.forward * -playerSpeed * Time.deltaTime;
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(new Vector3(0f, deltaRotate, 0f) * Time.deltaTime);
		if(Input.GetKey(KeyCode.LeftArrow))
				transform.Rotate(new Vector3(0f, -deltaRotate, 0f) * Time.deltaTime);
		//Debug.Log("Updated...");
	}
	
}