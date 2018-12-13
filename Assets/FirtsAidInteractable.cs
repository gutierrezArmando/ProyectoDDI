using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirtsAidInteractable : MonoBehaviour {
    
	private int cantidadInventario;
    public GameObject lblCantidadInventario;
    private bool isSelected;
    public GameObject healthIndicator;
    public GameObject MQTTProtocol;
	
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
                if (cantidadInventario < 3)
                {
                    cantidadInventario++;
                    lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text = cantidadInventario.ToString();
                    healthIndicator.GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0, 0);
                    Destroy(transform.root.gameObject);
                }
            }                
        }
            
	}

    public void SelectUnSelectFAID()
    {
        isSelected = !isSelected;
        print(isSelected);
    }

}
