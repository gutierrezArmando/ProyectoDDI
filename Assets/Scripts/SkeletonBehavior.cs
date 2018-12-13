using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBehavior : MonoBehaviour {

    public GameObject player;
    private Animator anim;
    private int life = 3;
    private AudioSource audioDanio;
    public GameObject healthIndicator;
    public float alphaLevel;
    private int qtyDanio=0;
    private bool charginAttack = false;
    public GameObject efectoEspada;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
        audioDanio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {

		if(Vector3.Distance(player.transform.position, this.transform.position) < 8)
        {
            Vector3 direction = player.transform.position - this.transform.position;
            direction.y = 0;
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(direction), 0.1f);
            anim.SetBool("isIdle", false);
            if(direction.magnitude > 2 && life>0)
            {
                this.transform.Translate(0,0,0.1f);
                anim.SetBool("isWalking", true);
                anim.SetBool("isAttacking", false);          
            }
            else
            {
                anim.SetBool("isAttacking", true);
                anim.SetBool("isWalking", false);
                if(!charginAttack)
                {
                    StartCoroutine(WaitForNextAttack());
                    StartCoroutine(DamageOnPlayer());
                    return;
                }
                Debug.Log(qtyDanio);
            }
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
            anim.SetBool("isAttacking", false);
            
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            if (life > 0)
            {
                StartCoroutine(Damage());
                print("Test");
            }
            else
            {
                anim.SetBool("isIdle", false);
                anim.SetBool("isWalking", false);
                anim.SetBool("isAttacking", false);
                anim.SetBool("isDeath", true);
                Destroy(transform.root.gameObject, 5f);
            }
        }            
    }

    IEnumerator Damage()
    {
        life--;
        anim.SetBool("isDamage", true);
        audioDanio.Play();
        yield return new WaitForSeconds(0.25f);
        anim.SetBool("isDamage", false);
        print("Test: " + life);
        audioDanio.Stop();
    }

    IEnumerator WaitForNextAttack()
    {
        charginAttack = true;
        yield return new WaitForSeconds(2.9f);
        charginAttack = false;
        /*
        if(anim.GetBool("isAttacking")&& !anim.GetBool("isWalking"))
        {
            alphaLevel = healthIndicator.GetComponent<MeshRenderer>().material.color.a;
            alphaLevel += 0.05f;
            healthIndicator.GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0, alphaLevel);
            Debug.Log(alphaLevel);
        }*/
//            healthIndicator.GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0, alphaLevel);
    }
    IEnumerator DamageOnPlayer()
    {
        alphaLevel = healthIndicator.GetComponent<MeshRenderer>().material.color.a;
        alphaLevel += 0.05f;
        yield return new WaitForSeconds(1);
        if(charginAttack && anim.GetBool("isAttacking")&& !anim.GetBool("isWalking"))
        {

            qtyDanio++;
            ActualizarColorSalud();
        }
    }

    private void ActualizarColorSalud()
    {
        StartCoroutine(activarSonidoEspada());
        alphaLevel = healthIndicator.GetComponent<MeshRenderer>().material.color.a;
        alphaLevel += 0.05f;
        healthIndicator.GetComponent<MeshRenderer>().material.color = new Color(255f, 0, 0, alphaLevel);
    }
    IEnumerator activarSonidoEspada()
    {
        efectoEspada.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        efectoEspada.SetActive(false);
    }
}
