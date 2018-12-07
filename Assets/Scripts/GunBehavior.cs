using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBehavior : MonoBehaviour {

    private Animator anim;
    public GameObject efecto;
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
            efecto.SetActive(true);
        }
        else
        {
            anim.SetBool("isShooting", false);
            anim.SetBool("isNormalState", true);
            efecto.SetActive(false);
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
        anim.SetBool("isReload", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("isReload", false);
    }
}
