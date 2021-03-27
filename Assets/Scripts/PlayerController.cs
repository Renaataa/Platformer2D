using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField] private int jump;
    private bool isTagGround;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
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
        else if(Input.GetKeyUp(KeyCode.S)) CrouchJumpOff();
        
        if(Input.GetKeyDown(KeyCode.W)) Jump();
        else if(Input.GetKeyUp(KeyCode.W)) CrouchJumpOff();
    }

    void Crouch(){
        anim.SetInteger("Anim", 3);
        
        if(!sr.flipX){
            GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.07019526f, -0.1549015f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.2404226f, 0.3056974f);
        }
        else if(sr.flipX){
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.07049161f, -0.1583256f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.2399668f, 0.2988491f);
        }    
    }

    void Jump(){
        if(isTagGround){
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.000667572f, -0.01985767f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.2616048f, 0.3212546f);
        }
    }

    void CrouchJumpOff(){
        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.005184233f, -0.08056042f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.2300652f, 0.4543795f);
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "ground") {
            isTagGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "ground") {
            isTagGround = false;
        }
    }
}
