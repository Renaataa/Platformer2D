using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard : MonoBehaviour
{
    public GameObject fireball;
    SpriteRenderer sr;
    Rigidbody2D rb;
    Animator anim;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        if(GameObject.Find("Player").transform.position.x < transform.position.x)
            sr.flipX = false;
        else if(GameObject.Find("Player").transform.position.x > transform.position.x)
            sr.flipX = true;
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            anim.SetInteger("Wizard", 1);
            Progress.StartNewProgress(this.gameObject, 0.9f, SpawnFireball);
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            anim.SetInteger("Wizard", 0);
            Progress.StartNewProgress(this.gameObject, 0, SpawnFireball);
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            anim.SetInteger("Wizard", 2); 
            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;  
            Destroy(gameObject, 0.5f);
        }
    }

    void SpawnFireball(){
        if(sr.flipX == false){
            fireball.GetComponent<SpriteRenderer>().flipX = false;
            Instantiate(fireball, new Vector2(transform.position.x - 0.3f, transform.position.y), Quaternion.identity);
        }
        else if(sr.flipX == true){
            fireball.GetComponent<SpriteRenderer>().flipX = true;
            Instantiate(fireball, new Vector2(transform.position.x + 0.3f, transform.position.y), Quaternion.identity);
        }
    }
}
