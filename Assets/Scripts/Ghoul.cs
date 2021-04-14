using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghoul : MonoBehaviour
{
    Animator anim;
    SpriteRenderer sr;
    Rigidbody2D rb;
    public int distance;
    float maxDistance;
    float minDistance;
    float speed = 1f;

    private void Start (){
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        
        maxDistance = transform.position.x + distance;
        minDistance = transform.position.x - distance;
    }

    private void FixedUpdate(){
        transform.Translate(transform.right * speed * Time.deltaTime);
        
        if(transform.position.x > maxDistance){
            speed = -speed;
            sr.flipX = false;
        }
        else if(transform.position.x < minDistance){
            speed = -speed;
            sr.flipX = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            anim.SetTrigger("Ghoul");
            speed = 0;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezeRotation;
            Destroy(gameObject, 0.6f);
        }
    }

    void Destroy(){
        Destroy(gameObject);
    }
}
