using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour {

    private Animator anim;
    public GameObject efectoParticulas;
    public GameObject efectoSonidoBalas;
    public GameObject efectoSonidoRecarga;
    public GameObject BalaPrefab;
    public GameObject MQTTProtocol;

    public GameObject lblCantidadInventario;
    private int cantidadInventario;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        efectoParticulas.SetActive(false);
        efectoSonidoBalas.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Mouse0) ||  MQTTProtocol.GetComponent<M2MQTTTerminals>().msg=="Shoot")
        {
            cantidadInventario = int.Parse(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
            if (cantidadInventario > 0)
            {
                StartCoroutine(Shoot());
                return;
            }
        }
        if(Input.GetKeyDown(KeyCode.Mouse1) || MQTTProtocol.GetComponent<M2MQTTTerminals>().msg=="Reload")
        {
            //StartCoroutine(Reload());
            //return;
            //anim.SetBool("isReload", false);
        }
	}

    IEnumerator Reload()
    {
        efectoSonidoRecarga.SetActive(true);
        anim.SetBool("isReload", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("isReload", false);
        efectoSonidoRecarga.SetActive(false);
    }

    IEnumerator Shoot()
    {
        anim.SetBool("isShooting", true);
        anim.SetBool("isNormalState", false);
        efectoParticulas.SetActive(true);
        efectoSonidoBalas.SetActive(true);
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isShooting", false);
        anim.SetBool("isNormalState", true);
        efectoParticulas.SetActive(false);
        efectoSonidoBalas.SetActive(false);
    }

}
