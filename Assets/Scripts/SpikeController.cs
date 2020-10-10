using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : MonoBehaviour
{
    Animator anim;
    public bool hasHitBox;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            anim.SetBool("IsStandingOn", true);
        }
    }

    private void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.tag == "Player"){
            anim.SetBool("IsStandingOn", false);
        }
    }
}
