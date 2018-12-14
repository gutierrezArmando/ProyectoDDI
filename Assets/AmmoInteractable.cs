using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInteractable : MonoBehaviour {

    private int cantidadInventario;
    public GameObject lblCantidadInventario;
    private bool isSelected;
    public GameObject MQTTProtocol;
    private bool isPressed = false;

	// Use this for initialization
	void Start () {
        isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.Mouse1) || MQTTProtocol.GetComponent<M2MQTTTerminals>().msg=="Reload")
            {
                //print(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
                cantidadInventario = int.Parse(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
                print("Original: " + cantidadInventario);
                //if (cantidadInventario < 90)
                //{
                    cantidadInventario += 60;
                    print("Nuerva: " + cantidadInventario);
                    lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text = cantidadInventario.ToString();
                    Destroy(transform.root.gameObject);
                //}
            }
        }
	}


    public void SelectUnSelectAmmoBox()
    {
        isSelected = !isSelected;
        print(isSelected);
    }
}
