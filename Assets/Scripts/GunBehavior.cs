using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour {

    private Animator anim;
    public GameObject efectoParticulas;
    public GameObject efectoSonidoBalas;
    public GameObject efectoSonidoRecarga;
    public GameObject BalaPrefab;
    public float speed = 750f;

    public GameObject lblCantidadInventario;
    private int cantidadInventario;
    
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey(KeyCode.Mouse0))
        {
           
                StartCoroutine(Shoot());
                return;
            
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

    IEnumerator Shoot()
    {
        cantidadInventario = int.Parse(lblCantidadInventario.GetComponent<UnityEngine.UI.Text>().text);
        if (cantidadInventario > 0)
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
        //CreateBala();
        yield return new WaitForSeconds(0.2f);
    }

    void CreateBala()
    {
            GameObject bala = Instantiate(BalaPrefab, transform.position, Quaternion.identity) as GameObject;
            Rigidbody rigibodyBala = bala.GetComponent<Rigidbody>();
            bala.transform.Rotate(0, 0, -90f);
            //instBulletRigibody.AddForce(Vector3.forward * speed);
            rigibodyBala.AddForce(Camera.main.transform.forward * speed);
            Destroy(bala, 10f);
    }
}
