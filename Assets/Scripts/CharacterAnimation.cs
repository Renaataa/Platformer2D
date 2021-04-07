using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    void Start(){
        anim = GetComponent<Animator>();
    }

    void Update(){

        if(Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow)){
            anim.SetBool("isWalking", true);
        } else {
            anim.SetBool("isWalking", false);
        }

        if(Input.GetKeyDown(KeyCode.UpArrow) && GetComponent<PlayerController2>().isGrounded == true){
            anim.SetTrigger("jump");
        }
    }
}
