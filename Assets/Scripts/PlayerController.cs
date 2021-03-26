using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    SpriteRenderer sr;

    public CircleCollider2D circleCollider;
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
        GetComponent<BoxCollider2D>().size = new Vector2(0.3199312f, 0.2900325f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.03044912f, -0.1549837f);
    }

    void Jump(){
        if(isTagGround){
            rb.AddForce(transform.up * jump, ForceMode2D.Impulse);
            anim.SetTrigger("Jump");
            //GetComponent<BoxCollider2D>().enabled = false;
            GetComponent<BoxCollider2D>().size = new Vector2(0.1803429f, 0.3297041f);
            GetComponent<BoxCollider2D>().offset = new Vector2(-0.008590907f, -0.02486522f);

            circleCollider.radius = 0.05686685f;
            circleCollider.offset = new Vector2(0.01013142f, -0.2965837f);
        }
    }

    void CrouchJumpOff(){
        GetComponent<BoxCollider2D>().size = new Vector2(0.2290478f, 0.3262739f);
        GetComponent<BoxCollider2D>().offset = new Vector2(-0.005111992f, -0.02317221f);
        //GetComponent<BoxCollider2D>().enabled = false;
        circleCollider.radius = 0.115769f;
        circleCollider.offset = new Vector2(-0.005348921f, -0.1860802f);
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
