using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Platformer : MonoBehaviour
{
    Rigidbody2D rb; 
    Animator anim;
    SpriteRenderer spriteRenderer;
    public float speed;
    public float invulTime = 5f;
    public float health = 5f;
    private bool isInvul = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        float moveByX = x * speed;
        float moveByY = y * speed;
        if(moveByX != 0 || moveByY != 0){
            anim.SetBool("IsWalking", true);
            if(moveByX < 0){
                spriteRenderer.flipX = true;
            } else {
                spriteRenderer.flipX = false;
            }
        } else {
            anim.SetBool("IsWalking", false);
        }
        rb.velocity = new Vector2(moveByX, moveByY);
    }

    private void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag == "Coin"){
            Destroy(col.gameObject);
        }
        
    }

    private void OnTriggerStay2D(Collider2D col){
        bool shouldTakeDamage = col.gameObject.GetComponent<SpikeController>().hasHitBox;
        if(shouldTakeDamage){
            ApplyDamage(1f);
        }
    }

    public void ApplyDamage(float dmg){
        if(!isInvul){
            health -= 1;
            StartCoroutine(JustHurt());
        }
    }

    IEnumerator JustHurt(){
        isInvul = true;
        yield return new WaitForSeconds(invulTime);
        isInvul = false;
    }



}
