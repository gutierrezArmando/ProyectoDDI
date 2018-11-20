using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalk : MonoBehaviour {

	public int playerSpeed = 2;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = transform.position + Camera.main.transform.forward * playerSpeed * Time.deltaTime;
        //Debug.Log("Updated...");
	}
}