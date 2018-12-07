using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour {

    private Animator anim;
    public GameObject efectoParticulas;
    public GameObject efectoSonidoBalas;
    public GameObject efectoSonidoRecarga;

    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Mouse0))
        {
            anim.SetBool("isShooting", true);
            anim.SetBool("isNormalState", false);
            efectoParticulas.SetActive(true);
            efectoSonidoBalas.SetActive(true);
        }
        else
        {
            anim.SetBool("isShooting", false);
            anim.SetBool("isNormalState", true);
            efectoParticulas.SetActive(false);
            efectoSonidoBalas.SetActive(false);
        }
        if(Input.GetKeyDown(KeyCode.Mouse1))
        {
            StartCoroutine(Reload());
            return;
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
}
