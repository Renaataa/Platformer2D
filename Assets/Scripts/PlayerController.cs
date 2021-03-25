using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;
    [SerializeField] private int jump;
    private bool isTagGround;
    //public BoxCollider2D boxCollider1;
    //public BoxCollider2D boxCollider2;

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
        
        if(Input.GetKeyDown(KeyCode.S)) Crouch();
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
        GetComponent<BoxCollider2D>().size = new Vector2(0.2290478f, 0.4017991f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.005111992f, -0.07970869f);
    }

    void Jump(){
        if(isTagGround){
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
        }
    }

    private void OnTriggerEnter2D(Collider2D other){
        if(other.tag == "ground") isTagGround = true;
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.tag == "ground") isTagGround = false;
    }
}
