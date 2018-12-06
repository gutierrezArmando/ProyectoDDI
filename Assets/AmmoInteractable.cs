using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInteractable : MonoBehaviour {

    private int cantidadInventario;
    public GameObject lblCantidadInventario;
    private bool isSelected;

	// Use this for initialization
	void Start () {
        isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (isSelected)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //print(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
                cantidadInventario = int.Parse(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
                print("Original: "+cantidadInventario);
                if (cantidadInventario < 1000)
                {
                    cantidadInventario+=30;
                    print("Nuerva: " + cantidadInventario);
                    lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text = cantidadInventario.ToString();
                    Destroy(transform.root.gameObject);
                }
            }
        }
	}


    public void SelectUnSelectAmmoBox()
    {
        isSelected = !isSelected;
        print(isSelected);
    }
}
