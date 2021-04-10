using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    void Start(){
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update(){

        if((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))){
            CrouchOff();
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && GetComponent<PlayerController2>().isGrounded == true){
            if(!Input.GetKeyDown(KeyCode.DownArrow) && !Input.GetKey(KeyCode.DownArrow)){
                CrouchOff();
                anim.SetTrigger("jump");
            }
        }

    
        if((Input.GetKeyDown(KeyCode.DownArrow)) && GetComponent<PlayerController2>().isGrounded == true){
            anim.SetTrigger("crouch");

            GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.0001967549f, -0.1565886f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.1009637f, 0.2951798f);  
            
            rb.constraints = RigidbodyConstraints2D.FreezePositionX|RigidbodyConstraints2D.FreezePositionY|RigidbodyConstraints2D.FreezeRotation;

            Invoke("CrouchOff", 0.35f);
        } 
    }

    void CrouchOff(){
        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.005184233f, -0.07877457f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.2300652f, 0.4508078f);

        rb.constraints = RigidbodyConstraints2D.None|RigidbodyConstraints2D.FreezeRotation;
    }
}
