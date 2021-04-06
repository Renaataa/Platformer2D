using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    [SerializeField] private int jump;
    public bool isTagGround;

    private void Start(){
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update(){
        rb.velocity = new Vector2(Input.GetAxis("Horizontal"), rb.velocity.y);

        if(Input.GetAxis("Horizontal") == 0){
            anim.SetInteger("Anim", 0);
        }
        else if(Input.GetAxis("Horizontal") > 0){
            anim.SetInteger("Anim", 1);
            sr.flipX = false;
            if(!isTagGround) anim.SetInteger("Anim", 2);
        }
        else if(Input.GetAxis("Horizontal") < 0){
            anim.SetInteger("Anim", 1);
            sr.flipX = true;
            if(!isTagGround) anim.SetInteger("Anim", 2);
        }
        
        if(Input.GetKeyDown(KeyCode.S)) Crouch();
        else if(Input.GetKeyUp(KeyCode.S)) CrouchJumpOff();

        if(Input.GetKeyDown(KeyCode.W)) Jump();
        else 
        {
            if(Input.GetKeyUp(KeyCode.W)) CrouchJumpOff();     
            //Debug.Log("here " + isTagGround);
            if(Input.GetButtonUp("Vertical") == true && isTagGround == true) {
                anim.SetBool("IsJumping", false);
                Debug.Log("here2");
            } 
        }
    }

    void Crouch(){
        anim.SetInteger("Anim", 3);
        
        if(sr.flipX == false){
            GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.07019526f, -0.1549015f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.2404226f, 0.3056974f);
        }
        else if(sr.flipX == true){
            GetComponent<CapsuleCollider2D>().offset = new Vector2(0.07049161f, -0.1583256f);
            GetComponent<CapsuleCollider2D>().size = new Vector2(0.2399668f, 0.2988491f);
        }    
    }

    void Jump(){
        if(isTagGround){
            anim.SetBool("IsJumping", true);
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            
            //GetComponent<CapsuleCollider2D>().offset = new Vector2(0.000667572f, -0.01985767f);
            //GetComponent<CapsuleCollider2D>().size = new Vector2(0.2616048f, 0.3212546f);
        }
    }

    void CrouchJumpOff(){
        GetComponent<CapsuleCollider2D>().offset = new Vector2(-0.005184233f, -0.07877457f);
        GetComponent<CapsuleCollider2D>().size = new Vector2(0.2300652f, 0.4508078f);
    }
}
