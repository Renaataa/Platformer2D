using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    public Joystick joystick;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private Rigidbody2D rb;
    private bool facingRight = true;
    public bool isGrounded;
    
    private int extraJumps;
    public int extraJumpsValue;

    private void Start(){
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate(){
        moveInput = joystick.Horizontal;
        rb.velocity = new Vector2(moveInput*speed, rb.velocity.y);

        if(facingRight == false && moveInput > 0){
            Flip();
        } else if(facingRight == true && moveInput < 0){
            Flip();
        }
    }

    private void Update(){
        if(isGrounded == true){
            extraJumps = extraJumpsValue;
        }

        if (joystick.Vertical > 0.3 && extraJumps > 0){
            rb.velocity = Vector2.up * jumpForce;
            extraJumps--;
        } else if(joystick.Vertical > 0.3 && extraJumps == 0 && isGrounded == true){
            rb.velocity = Vector2.up * jumpForce;
        }
    }
    void Flip(){
        facingRight = !facingRight;
        Vector3 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}
