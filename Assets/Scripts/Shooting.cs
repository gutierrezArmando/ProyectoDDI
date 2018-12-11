﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour {

    public GameObject bullet;
    public float speed = 1250f;

    public GameObject lblCantidadInventario;

    private int cantidadInventario;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            cantidadInventario = int.Parse(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
            if(cantidadInventario>0)
                Shoot();
        }
	}

    void Shoot()
    {
        GameObject instBullet = Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        Rigidbody instBulletRigibody = instBullet.GetComponent<Rigidbody>();
        instBullet.transform.Rotate(0, 0, -90f);
        instBulletRigibody.AddForce(Camera.main.transform.forward * speed);
        Destroy(instBullet, 10f);

        
        cantidadInventario--;
        lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text = cantidadInventario.ToString();

    }
}
