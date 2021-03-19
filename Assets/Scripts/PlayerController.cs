﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    //public BoxCollider2D boxCollider1;
    //public BoxCollider2D boxCollider2;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate(){
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), rb.velocity.y);
        
        if(Input.GetAxis("Horizontal") == 0)
            anim.SetInteger("Anim", 0);
        else if(Input.GetAxis("Horizontal") > 0){
            anim.SetInteger("Anim", 1);
            sr.flipX = false;
        }
        else if(Input.GetAxis("Horizontal") < 0){
            anim.SetInteger("Anim", 1);
            sr.flipX = true;
        }
        
        if(Input.GetKey(KeyCode.S)) Crouch();
        else CrouchOff();
        
        if(Input.GetKeyDown(KeyCode.W))
            Jump();
    }

    void Crouch(){
        anim.SetInteger("Anim", 3);
        GetComponent<BoxCollider2D>().size = new Vector2(0.3199312f, 0.2900325f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.03044912f, -0.1549837f);
    }

    void CrouchOff(){
        GetComponent<BoxCollider2D>().size = new Vector2(0.2290478f, 0.4405826f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.005111992f, -0.07970869f);
    }

    void Jump(){
        anim.SetInteger("Anim", 2);
        rb.AddForce(transform.up, ForceMode2D.Impulse);
    }
}
